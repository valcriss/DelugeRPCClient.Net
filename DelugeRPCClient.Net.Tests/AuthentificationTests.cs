using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class AuthentificationTests
    {
        [TestMethod]        
        public async Task LoginLogout()
        {
            DelugeClient client = new DelugeClient(url: "http://localhost:8112/json", password: "deluge");
            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);
            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
