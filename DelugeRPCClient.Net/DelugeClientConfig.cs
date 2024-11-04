using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net
{
    public class DelugeClientConfig
    {
        public DelugeClientConfig() { }

        public bool IgnoreSslErrors { get; set; } = true;

        public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(30);
    }
}
