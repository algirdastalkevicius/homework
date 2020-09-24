using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationManager.Models
{
    public class Configuration
    {
        public IDictionary<string, string> ConfigData { get; private set; }

        public Configuration()
        {
            ConfigData = new Dictionary<string, string>();
        }

        public bool TryGetConfiguration(string key, out string result)
        {
            if(ConfigData.TryGetValue(key, out var res))
            {
                result = res;
                return true;
            }
            else
            {
                result = "Error";
                return false;
            }
        }

    }
}
