using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelugeRPCClient.Net.Models
{
    internal static class Extensions
    {
        public static List<String> GetAllJsonPropertyFromType(this Type t)
        {
            var type = typeof(JsonPropertyAttribute);
            var props = t.GetProperties().Where(prop => Attribute.IsDefined(prop, type)).ToList();
            return props.Select(x => x.GetCustomAttributes(type, true).Single()).Cast<JsonPropertyAttribute>().Select(x => x.PropertyName).ToList();
        }
    }
}
