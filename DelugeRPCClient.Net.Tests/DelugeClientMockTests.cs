using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Tests
{
    [TestClass]
    public class DelugeClientMockTests
    {
        private HttpClient CreateClient()
        {
            var handler = new MockHttpMessageHandler(request =>
            {
                var json = request.Content.ReadAsStringAsync().Result;
                dynamic payload = JsonConvert.DeserializeObject(json);
                int id = payload.id;
                string method = payload.method;
                object result = method switch
                {
                    "auth.login" => true,
                    "auth.delete_session" => true,
                    "core.pause_torrent" => null,
                    "core.resume_torrent" => null,
                    _ => null
                };
                var responseJson = JsonConvert.SerializeObject(new { id, result, error = (object)null });
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
                };
            });
            return new HttpClient(handler)
            {
                BaseAddress = new Uri("http://mock")
            };
        }

        [TestMethod]
        public async Task LoginLogoutWithMock()
        {
            using var httpClient = CreateClient();
            var client = new DelugeClient("http://mock", "pwd", httpClient: httpClient);
            bool login = await client.Login();
            Assert.IsTrue(login);
            bool logout = await client.Logout();
            Assert.IsTrue(logout);
        }

        [TestMethod]
        public async Task PauseResumeWithMock()
        {
            using var httpClient = CreateClient();
            var client = new DelugeClient("http://mock", "pwd", httpClient: httpClient);
            bool pause = await client.PauseTorrent("hash");
            Assert.IsTrue(pause);
            bool resume = await client.ResumeTorrent("hash");
            Assert.IsTrue(resume);
        }
    }
}
