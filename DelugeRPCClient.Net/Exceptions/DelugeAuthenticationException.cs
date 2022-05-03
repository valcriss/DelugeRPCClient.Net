using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Exceptions
{
    internal class DelugeAuthenticationException : Core.Exceptions.DelugeClientException
    {
        public DelugeAuthenticationException() : base("Unable to authenticate")
        {
        }
    }
}
