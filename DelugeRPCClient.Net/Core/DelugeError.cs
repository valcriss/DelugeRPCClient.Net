using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Core
{
    internal class DelugeError
    {
        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
    }
}
