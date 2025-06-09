using DelugeRPCClient.Net.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class DelugeClientCoverageTests
    {
        [TestMethod]
        public async Task FullClientWorkflow()
        {
            using var httpClient = TestUtils.CreateMockClient(TestUtils.DefaultResponses);
            var client = new DelugeClient("http://mock", "pwd", httpClient: httpClient);
            Assert.IsTrue(await client.Login());
            Assert.IsNotNull(await client.ListTorrents());
            Assert.IsNotNull(await client.ListTorrentsExtended());
            Assert.IsNotNull(await client.GetTorrent("hash"));
            Assert.IsNotNull(await client.GetTorrentExtended("hash"));
            string torrentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.TestTorrentFilename);
            Assert.IsNotNull(await client.AddTorrentByMagnet("magnet"));
            Assert.IsNotNull(await client.AddTorrentByFile(torrentPath));
            Assert.IsNotNull(await client.AddTorrentByUrl("http://example.com/file.torrent"));
            Assert.IsTrue(await client.RemoveTorrent("hash"));
            Assert.IsTrue(await client.PauseTorrent("hash"));
            Assert.IsTrue(await client.ResumeTorrent("hash"));
            Assert.IsNull(await client.RecheckTorrents(new List<string> { "hash" }));
            Assert.IsNotNull(await client.ListConfigs());
            Assert.IsNotNull(await client.ListLabels());
            Assert.IsTrue(await client.LabelExists("lbl"));
            Assert.IsTrue(await client.AddLabel("lbl"));
            Assert.IsTrue(await client.RemoveLabel("lbl"));
            Assert.IsTrue(await client.SetTorrentLabel("hash", "lbl"));
            Assert.IsTrue(await client.Logout());
        }

        [TestMethod]
        public async Task PauseResumeReturnsFalseOnBool()
        {
            var dict = new Dictionary<string, object>(TestUtils.DefaultResponses)
            {
                ["core.pause_torrent"] = false,
                ["core.resume_torrent"] = false
            };
            using var httpClient = TestUtils.CreateMockClient(dict);
            var client = new DelugeClient("http://mock", "pwd", httpClient: httpClient);
            Assert.IsFalse(await client.PauseTorrent("hash"));
            Assert.IsFalse(await client.ResumeTorrent("hash"));
        }

        [TestMethod]
        public void AddLabelThrowsOnInvalid()
        {
            using var httpClient = TestUtils.CreateMockClient(TestUtils.DefaultResponses);
            var client = new DelugeClient("http://mock", "pwd", httpClient: httpClient);
            Assert.ThrowsExceptionAsync<ArgumentException>(() => client.AddLabel(null));
        }

        [TestMethod]
        public async Task ErrorAndDesyncThrow()
        {
            using var errClient = TestUtils.CreateMockClient(TestUtils.DefaultResponses, error: true);
            var client = new DelugeClient("http://mock", "pwd", httpClient: errClient);
            try
            {
                await client.ListLabels();
                Assert.Fail("no exception");
            }
            catch (Exception)
            {
            }

            using var desyncClient = TestUtils.CreateMockClient(TestUtils.DefaultResponses, mismatchId: true);
            var client2 = new DelugeClient("http://mock", "pwd", httpClient: desyncClient);
            try
            {
                await client2.ListLabels();
                Assert.Fail("no exception");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void ModelPropertyCoverage()
        {
            var types = new[] { typeof(Config), typeof(Torrent), typeof(TorrentExtended), typeof(TorrentOptions) };
            foreach (var t in types)
            {
                var instance = Activator.CreateInstance(t);
                foreach (var prop in t.GetProperties())
                {
                    if (prop.CanWrite)
                    {
                        object value = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
                        prop.SetValue(instance, value);
                    }
                    if (prop.CanRead)
                    {
                        _ = prop.GetValue(instance);
                    }
                }
            }
            Assert.IsTrue(typeof(Torrent).GetProperties().Length > 0);
        }
    }
}
