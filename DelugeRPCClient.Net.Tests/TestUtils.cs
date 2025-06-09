using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DelugeRPCClient.Net.Tests
{
    internal static class TestUtils
    {
        public static HttpClient CreateMockClient(Dictionary<string, object> responses, bool mismatchId = false, bool error = false)
        {
            var handler = new MockHttpMessageHandler(request =>
            {
                var json = request.Content.ReadAsStringAsync().Result;
                dynamic payload = JsonConvert.DeserializeObject(json);
                int id = payload.id;
                string method = payload.method;
                responses.TryGetValue(method, out object result);
                var responseObj = new
                {
                    id = mismatchId ? id + 1 : id,
                    result = error ? null : result,
                    error = error ? new { message = "err", code = 1 } : null
                };
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(responseObj), Encoding.UTF8, "application/json")
                };
            });
            return new HttpClient(handler) { BaseAddress = new Uri("http://mock") };
        }

        public static Dictionary<string, object> DefaultResponses => new()
        {
            ["auth.login"] = true,
            ["auth.delete_session"] = true,
            ["core.get_torrents_status"] = new Dictionary<string, DelugeRPCClient.Net.Models.Torrent>
            {
                ["hash"] = new DelugeRPCClient.Net.Models.Torrent { Hash = "hash", Name = "t", Paused = false, Ratio = 0, Message = "m", Label = "lbl" }
            },
            ["core.add_torrent_magnet"] = "hash",
            ["core.add_torrent_file"] = "hash",
            ["web.download_torrent_from_url"] = "temp.torrent",
            ["web.add_torrents"] = new List<List<object>> { new() { true, "hash" } },
            ["core.remove_torrent"] = true,
            ["core.pause_torrent"] = null,
            ["core.resume_torrent"] = null,
            ["core.force_recheck"] = null,
            ["core.get_config"] = new DelugeRPCClient.Net.Models.Config(),
            ["label.get_labels"] = new List<string> { "lbl" },
            ["label.add"] = null,
            ["label.remove"] = null,
            ["label.set_torrent"] = null
        };
    }
}
