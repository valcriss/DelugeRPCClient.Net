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
    public class ConfigTests
    {
        [TestMethod]
        public async Task ListConfigs()
        {
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);

            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);

            Config configs = await client.ListConfigs();
            Assert.IsNotNull(configs);

            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
