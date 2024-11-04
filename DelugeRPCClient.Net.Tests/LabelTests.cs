using DelugeRPCClient.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class LabelTests : DelugeClientTest
    {
        [TestMethod]
        public async Task ListLabels()
        {
            DelugeClient client = await Login();
            
            await AddTestLabel(client);
            
            List<string> labels = await client.ListLabels();
            Assert.IsNotNull(labels);
            Assert.AreNotEqual(0, labels.Count);
            
            await RemoveTestLabel(client);

            await Logout(client);
        }

        [TestMethod]
        public async Task AddAndRemoveLabel()
        {
            DelugeClient client = await Login();

            await AddTestLabel(client);

            await RemoveTestLabel(client);

            await Logout(client);
        }

        [TestMethod]
        public async Task AssignLabel()
        {
            DelugeClient client = await Login();

            await AddTestLabel(client);

            Torrent testTorrent = await AddTestTorrent(client);

            bool assignResult = await client.SetTorrentLabel(testTorrent.Hash, Constants.TestLabelName);
            Assert.IsTrue(assignResult);
            Thread.Sleep(1000);

            bool unsertLabelResult = await client.SetTorrentLabel(testTorrent.Hash, null);
            Assert.IsTrue(unsertLabelResult);
            Thread.Sleep(1000);

            await RemoveTestLabel(client);

            await RemoveTestTorrent(client, testTorrent);          

            await Logout(client);
        }
    }
}
