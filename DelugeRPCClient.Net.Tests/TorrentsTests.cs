using DelugeRPCClient.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class TorrentsTests : DelugeClientTest
    {
        [TestMethod]
        public async Task ListAndGetTorrent()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent testTorrent = await AddTestTorrent(client);

            List<Torrent> torrents = await client.ListTorrents();
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);

            Torrent torrent = await client.GetTorrent(torrents[0].Hash);
            Assert.IsNotNull(torrent);

            await RemoveTestTorrent(client, testTorrent);

            await Logout(client);
        }

        [TestMethod]
        public async Task ListAndGetTorrentExtended()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent testTorrent = await AddTestTorrent(client);

            List<TorrentExtended> torrents = await client.ListTorrentsExtended();
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);

            TorrentExtended torrent = await client.GetTorrentExtended(torrents[0].Hash);
            Assert.IsNotNull(torrent);

            await RemoveTestTorrent(client, testTorrent);

            await Logout(client);
        }

        [TestMethod]
        public async Task AddRemoveTorrentByMagnet()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent torrent = await client.AddTorrentByMagnet(Constants.TorrentMagnet);
            Assert.IsNotNull(torrent);

            await Task.Delay(1000);

            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);

            await Logout(client);
        }

        [TestMethod]
        public async Task AddRemoveTorrentByFile()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent torrent = await client.AddTorrentByFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.TestTorrentFilename));
            Assert.IsNotNull(torrent);

            await Task.Delay(1000);

            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);

            await Logout(client);
        }

        [TestMethod]
        public async Task AddRemoveTorrentByUrl()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent torrent = await client.AddTorrentByUrl(Constants.TestTorrentUrl);
            Assert.IsNotNull(torrent);

            await Task.Delay(1000);

            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);

            await Logout(client);
        }

        [TestMethod]
        public async Task PauseResumeTorrent()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent testTorrent = await AddTestTorrent(client);

            List<Torrent> torrents = await client.ListTorrents();
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);

            Torrent torrent = torrents[0];

            if(torrent.Paused)
            {
                bool resumeResult = await client.ResumeTorrent(torrent.Hash);
                Assert.IsTrue(resumeResult);
                bool pauseResult = await client.PauseTorrent(torrent.Hash);
                Assert.IsTrue(pauseResult);
            }
            else
            {
                bool pauseResult = await client.PauseTorrent(torrent.Hash);
                Assert.IsTrue(pauseResult);
                bool resumeResult = await client.ResumeTorrent(torrent.Hash);
                Assert.IsTrue(resumeResult);              
            }

            await RemoveTestTorrent(client, testTorrent);

            await Logout(client);
        }

        [TestMethod]
        public async Task RecheckTorrents()
        {
            SkipIfNoIntegration();
            DelugeClient client = await Login();

            Torrent testTorrent = await AddTestTorrent(client);

            List<Torrent> torrents = await client.ListTorrents();
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);

            Torrent torrent = torrents[0];

            bool? recheckResult = await client.RecheckTorrents(torrent.Hash.Split(",").ToList());
            Assert.IsNull(recheckResult);

            await RemoveTestTorrent(client, testTorrent);

            await Logout(client);
        }
    }
}
