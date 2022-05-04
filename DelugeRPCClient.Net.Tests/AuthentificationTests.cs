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
            DelugeClient client = new DelugeClient(url: Constants.DelugeUrl, password: Constants.DelugePassword);
            bool loginResult = await client.Login();
            Assert.IsTrue(loginResult);
            bool logoutResult = await client.Logout();
            Assert.IsTrue(logoutResult);
        }
    }
}
