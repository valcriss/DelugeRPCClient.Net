using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Core
{
    internal class DelugeResponsee<T>
    {
        [JsonProperty(PropertyName = "id")]
        public int ResponseId { get; set; }

        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "error")]
        public DelugeError Error { get; set; }
    }
}
