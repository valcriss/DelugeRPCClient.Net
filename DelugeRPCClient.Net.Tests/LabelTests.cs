using DelugeRPCClient.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class LabelTests
    {
        [TestMethod]
        public async Task ListLabels()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            List<string> labels = await client.ListLabels();
            Assert.IsNotNull(labels);
            Assert.AreNotEqual(0, labels.Count);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        [TestMethod]
        public async Task AddRemoveLabel()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            bool addLabelResult = await client.AddLabel(Constants.TestLabelName);
            Assert.IsTrue(addLabelResult);

            Thread.Sleep(1000);

            bool removeLabelResult = await client.RemoveLabel(Constants.TestLabelName);
            Assert.IsTrue(removeLabelResult);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        [TestMethod]
        public async Task AssignLabel()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            List<Torrent> torrents = await client.ListTorrents(new Dictionary<string, string>() { { "label", "" } });
            Assert.IsNotNull(torrents);
            Assert.AreNotEqual(0, torrents.Count);
            
            Torrent torrent = torrents[0];

            bool assignResult = await client.SetTorrentLabel(torrent.Hash, Constants.TestLabelName);
            Assert.IsTrue(assignResult);
            Thread.Sleep(1000);

            bool unsertLabelResult = await client.SetTorrentLabel(torrent.Hash, null);
            Assert.IsTrue(unsertLabelResult);
            Thread.Sleep(1000);

            bool removeLabelResult = await client.RemoveLabel(Constants.TestLabelName);
            Assert.IsTrue(removeLabelResult);
            Thread.Sleep(1000);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
