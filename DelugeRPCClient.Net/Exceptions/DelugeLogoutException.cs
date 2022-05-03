using System;
using System.Collections.Generic;
using System.Text;

namespace DelugeRPCClient.Net.Exceptions
{
    internal class DelugeLogoutException : Core.Exceptions.DelugeClientException
    {
        public DelugeLogoutException() : base("Unable to logout")
        {
        }
    }
}
