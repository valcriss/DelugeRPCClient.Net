using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Core.Exceptions
{
    internal class DelugeClientException : Exception
    {
        public DelugeClientException(string message) : base(message)
        {
            
        }
    }
}
