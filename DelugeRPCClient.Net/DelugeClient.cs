using DelugeRPCClient.Net.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net
{
    public class DelugeClient : Core.CoreDelugeWebClient
    {
        #region Private Variables

        private string Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// DelugeClient Constructor
        /// </summary>
        /// <param name="url">url of delugeweb (like http://localhost:8112/json)</param>
        /// <param name="password">delugeweb password</param>
        public DelugeClient(string url, string password) : base(url)
        {
            Password = password;
        }

        #endregion

        #region Authentification

        /// <summary>
        /// Start a session
        /// </summary>
        /// <returns>true if the session is started</returns>
        public async Task<bool> Login()
        {
            return await SendRequest<bool>("auth.login", Password);
        }

        /// <summary>
        /// Close the current session
        /// </summary>
        /// <returns>true if the session is successfully closed</returns>
        public async Task<bool> Logout()
        {
            return await SendRequest<bool>("auth.delete_session");
        }

        #endregion

        #region Torrents

        /// <summary>
        /// List all torrents optionnaly filtered
        /// </summary>
        /// <param name="filters">optional filters</param>
        /// <returns>List of torrents</returns>
        public async Task<List<Torrent>> ListTorrents(Dictionary<string, string> filters = null)
        {
            filters = filters ?? new Dictionary<string, string>();
            var keys = typeof(Torrent).GetAllJsonPropertyFromType();
            Dictionary<string, Torrent> result = await SendRequest<Dictionary<string, Torrent>>("core.get_torrents_status", filters, keys);
            return result.Values.ToList();
        }

        /// <summary>
        /// Get torrent informations by torrent hash
        /// </summary>
        /// <param name="hash">The requested torrent hash</param>
        /// <returns>the torrent object</returns>
        public async Task<Torrent> GetTorrent(string hash)
        {
            List<Torrent> torrents = await ListTorrents(new Dictionary<string, string>() { { "hash", hash } });
            return torrents.Count > 0 ? torrents[0] : null;
        }

        /// <summary>
        /// Add a new torrent by magnet information
        /// </summary>
        /// <param name="magnet">magnet information</param>
        /// <param name="options">Optional torrent options</param>
        /// <returns>the torrent object</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Torrent> AddTorrentByMagnet(string magnet, TorrentOptions options = null)
        {
            if (String.IsNullOrWhiteSpace(magnet)) throw new ArgumentException(nameof(magnet));
            var request = CreateRequest("core.add_torrent_magnet", magnet, options);
            request.NullValueHandling = NullValueHandling.Ignore;
            string hash = await SendRequest<string>(request);
            return await GetTorrent(hash);
        }

        /// <summary>
        /// Add a new torrent by .torrent file
        /// </summary>
        /// <param name="file">path of the .torrent file</param>
        /// <param name="options">Optional torrent options</param>
        /// <returns>the torrent object</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Torrent> AddTorrentByFile(string file, TorrentOptions options = null)
        {
            if (String.IsNullOrWhiteSpace(file)) throw new ArgumentException(nameof(file));
            if (!File.Exists(file)) throw new ArgumentException(nameof(file));
            string filename = Path.GetFileName(file);
            string base64 = Convert.ToBase64String(File.ReadAllBytes(file));
            var request = CreateRequest("core.add_torrent_file", filename, base64, options);
            request.NullValueHandling = NullValueHandling.Ignore;
            string hash = await SendRequest<string>(request);
            return await GetTorrent(hash);
        }

        /// <summary>
        /// Remove a torrent from deluge
        /// </summary>
        /// <param name="hash">The torrent's hash to be deleted</param>
        /// <param name="removeData">Optional, also remove data from disk</param>
        /// <returns>true if the torrent was successfully removed</returns>
        public async Task<bool> RemoveTorrent(string hash, bool removeData = false)
        {
            return await SendRequest<bool>("core.remove_torrent", hash, removeData);
        }

        /// <summary>
        /// Pause an active torrent
        /// </summary>
        /// <param name="hash">Hash of the target torrent</param>
        /// <returns>true if the action is successfull</returns>
        public async Task<bool> PauseTorrent(string hash)
        {
            bool? result =  await SendRequest<bool?>("core.pause_torrent", hash);
            Thread.Sleep(3000);
            return result == null;
        }

        /// <summary>
        /// Resume a paused torrent
        /// </summary>
        /// <param name="hash">Hash of the target torrent</param>
        /// <returns>true if the action is successfull</returns>
        public async Task<bool> ResumeTorrent(string hash)
        {
            bool? result = await SendRequest<bool?>("core.resume_torrent", hash);
            Thread.Sleep(3000);
            return result == null;
        }

        #endregion

        #region Labels

        /// <summary>
        /// List all existing labels
        /// </summary>
        /// <returns>list of labels</returns>
        public async Task<List<string>> ListLabels()
        {
            return await SendRequest<List<string>>("label.get_labels");
        }

        /// <summary>
        /// Looks of the existence of a specific label
        /// </summary>
        /// <param name="label">needle</param>
        /// <returns>Return true if the label exists</returns>
        public async Task<bool> LabelExists(string label)
        {
            List<string> labels = await ListLabels();
            return labels.Contains(label);
        }

        /// <summary>
        /// Create a new label on deluge
        /// </summary>
        /// <param name="label">The label name to be created</param>
        /// <returns>return true if the label is created</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddLabel(string label)
        {
            if (String.IsNullOrWhiteSpace(label)) throw new ArgumentException(nameof(label));
            bool? result = await SendRequest<bool?>(CreateRequest("label.add", label));
            return result == null;
        }

        /// <summary>
        /// Remove a existing label
        /// </summary>
        /// <param name="label">The label name to be removed</param>
        /// <returns>return true if the label is deleted</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> RemoveLabel(string label)
        {
            if (String.IsNullOrWhiteSpace(label)) throw new ArgumentException(nameof(label));
            bool? result = await SendRequest<Boolean?>(CreateRequest("label.remove", label));
            return result == null;
        }

        /// <summary>
        /// Assign a label to a torrent
        /// </summary>
        /// <param name="torrentId">The torrent hash</param>
        /// <param name="label">the label to be associated</param>
        /// <returns>True if the label is successfully associated</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> SetTorrentLabel(string torrentId, string label)
        {
            if (String.IsNullOrWhiteSpace(torrentId)) throw new ArgumentException(nameof(torrentId));
            bool exists = string.IsNullOrEmpty(label) ? true : await LabelExists(label);
            if(!exists)
            {
                await AddLabel(label);
            }
            var req = CreateRequest("label.set_torrent", torrentId, label);
            req.NullValueHandling = NullValueHandling.Include;
            bool? result = await SendRequest<Boolean?>(req);
            return result == null;
        }

        #endregion
    }
}
