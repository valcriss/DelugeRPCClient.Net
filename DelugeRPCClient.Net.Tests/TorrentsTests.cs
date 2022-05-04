using DelugeRPCClient.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class TorrentsTests
    {
        [TestMethod]
        public async Task ListAndGetTorrent()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            List<Torrent> torrents = await client.ListTorrents();
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);

            Torrent torrent = await client.GetTorrent(torrents[0].Hash);
            Assert.IsNotNull(torrent);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        [TestMethod]
        public async Task AddRemoveTorrentByMagnet()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            Torrent torrent = await client.AddTorrentByMagnet(Constants.TorrentMagnet);
            Assert.IsNotNull(torrent);

            Thread.Sleep(1000);

            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        [TestMethod]
        public async Task AddRemoveTorrentByFile()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            Torrent torrent = await client.AddTorrentByFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.TestTorrentFilename));
            Assert.IsNotNull(torrent);

            Thread.Sleep(1000);

            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        [TestMethod]
        public async Task PauseResumeTorrent()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

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

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
