using DelugeRPCClient.Net.Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelugeRPCClient.Net.Core
{
    public class CoreDelugeWebClient
    {
        private string Url { get; set; }
        private HttpClientHandler HttpClientHandler { get; set; }
        private HttpClient HttpClient { get; set; }
        private int RequestId { get; set; }
        private DelugeClientConfig DelugeClientConfig { get; set; }

        public CoreDelugeWebClient(string url, DelugeClientConfig config = null)
        {
            DelugeClientConfig = config ?? new DelugeClientConfig();

            HttpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = new CookieContainer(),
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            if (DelugeClientConfig.IgnoreSslErrors)
            {
                HttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            }

            HttpClient = new HttpClient(HttpClientHandler, true);
            HttpClient.Timeout = DelugeClientConfig.Timeout;

            RequestId = 1;

            Url = url;
        }

        protected async Task<T> SendRequest<T>(string method, params object[] parameters)
        {
            return await SendRequest<T>(CreateRequest(method, parameters));
        }

        protected async Task<T> SendRequest<T>(DelugeRequest webRequest)
        {
            var requestJson = JsonConvert.SerializeObject(webRequest, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = webRequest.NullValueHandling
            });


            var responseJson = await PostJson(requestJson);

            var webResponse = JsonConvert.DeserializeObject<DelugeResponsee<T>>(responseJson);

            if (webResponse.Error != null) throw new DelugeClientException(webResponse.Error.Message);
            if (webResponse.ResponseId != webRequest.RequestId) throw new DelugeClientException("Desync.");

            return webResponse.Result;
        }

        private async Task<String> PostJson(String json)
        {
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
           
            var responseMessage = await HttpClient.PostAsync(Url, content);
            responseMessage.EnsureSuccessStatusCode();

            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            return responseJson;
        }

        protected DelugeRequest CreateRequest(string method, params object[] parameters)
        {
            if (String.IsNullOrWhiteSpace(method)) throw new ArgumentException(nameof(method));
            return new DelugeRequest(RequestId++, method, parameters);
        }
    }
}
