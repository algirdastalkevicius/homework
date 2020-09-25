using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationLibrary.Models
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
            result = null;

            if (ConfigData.TryGetValue(key, out var res))
            {
                result = res;
                return true;
            }
            
            return false;
        }

    }
}
