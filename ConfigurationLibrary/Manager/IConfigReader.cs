using ConfigurationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLibrary.Manager
{
    public interface IConfigReader
    {
        public IDictionary<string, string> GetConfiguration(IReadOnlyCollection<string> keys);
        public ConfigResponse<T> GetValue<T>(string key);
        public IDictionary<string, string> GetFullConfiguration();
    }
}
