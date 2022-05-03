using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Models
{
    public class TorrentOptions
    {
        [JsonProperty(PropertyName = "move_completed_path")]
        public String MoveCompletedPath { get; set; }
    }
}
