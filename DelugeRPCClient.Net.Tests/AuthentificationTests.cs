using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class AuthentificationTests : DelugeClientTest
    {
        [TestMethod]        
        public async Task LoginLogout()
        {
            DelugeClient client = await Login();
            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
