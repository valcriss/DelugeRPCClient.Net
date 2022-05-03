using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Models
{
    public class Torrent
    {
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "is_seed")]
        public bool IsSeed { get; set; }

        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; set; }

        [JsonProperty(PropertyName = "paused")]
        public bool Paused { get; set; }

        [JsonProperty(PropertyName = "ratio")]
        public double Ratio { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
