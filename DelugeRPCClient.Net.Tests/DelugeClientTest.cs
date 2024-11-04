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
    public class DelugeClientTest
    {
        protected async Task<DelugeClient> Login()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);
            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            return client;
        }

        protected async Task Logout(DelugeClient client)
        {
            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }

        protected async Task<Torrent> AddTestTorrent(DelugeClient client)
        {
            Torrent torrent = await client.AddTorrentByFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.TestTorrentFilename));
            Assert.IsNotNull(torrent);
            Thread.Sleep(1000);
            return torrent;
        }

        protected async Task RemoveTestTorrent(DelugeClient client, Torrent torrent)
        {
            bool removeTorrentResult = await client.RemoveTorrent(torrent.Hash);
            Assert.IsTrue(removeTorrentResult);
            Thread.Sleep(1000);
        }

        protected async Task AddTestLabel(DelugeClient client)
        {
            bool addLabelResult = await client.AddLabel(Constants.TestLabelName);
            Assert.IsTrue(addLabelResult);
            Thread.Sleep(1000);
        }

        protected async Task RemoveTestLabel(DelugeClient client)
        {
            bool removeLabelResult = await client.RemoveLabel(Constants.TestLabelName);
            Assert.IsTrue(removeLabelResult);
            Thread.Sleep(1000);
        }
    }
}
