using ConfigurationManager.Factories;
using ConfigurationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConfigurationManager.Manager
{
    public class ConfigManager : IConfigReader, IConfigWriter
    {
        private readonly IDictionary<int, Configuration> _configurationLayers;

        public ConfigManager(ILayeredConfigurationFactory factory)
        {
            _configurationLayers = factory.GetLayeredConfiguration();
        }

        public void AddConfigurationLayer(int id, IDictionary<string, string> layer)
        {
            if (!_configurationLayers.ContainsKey(id))
                _configurationLayers[id] = new Configuration();

            foreach (var item in layer)
            {
                _configurationLayers[id].ConfigData[item.Key] = item.Value;
            }
        }

        public IDictionary<string, string> GetConfiguration(IReadOnlyCollection<string> keys)
        {
            var result = new Dictionary<string, string>();

            var distinctKeys = keys.Distinct();

            foreach (var key in distinctKeys)
            {
                result.Add(key, GetHighestLayerInformation(key));
            }

            return result;
        }

        public IDictionary<string, string> GetFullConfiguration()
        {
            var result = new Dictionary<string, string>();
            var keys = new HashSet<string>();

            foreach(var layer in _configurationLayers)
            {
                foreach(var item in layer.Value.ConfigData)
                {
                    if (!keys.Contains(item.Key))
                    {
                        keys.Add(item.Key);
                    }
                }
            }

            foreach (var key in keys)
            {
                result.Add(key, GetHighestLayerInformation(key));
            }

            return result;
        }

        public ConfigResponse<T> GetValue<T>(string key)
        {
            object result;
            var value = GetHighestLayerInformation(key);
            try
            {
                if (value != "Error")
                {
                    result = Convert.ChangeType(value, typeof(T));
                }
                else
                {
                    var response = new ConfigResponse<T> { Succeeded = false };
                    response.Errors.Add("Configuration value not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                var response = new ConfigResponse<T> { Succeeded = false };
                response.Errors.Add(ex.Message);
                return response;
            }

            return new ConfigResponse<T> { Succeeded = true, Value = (T)result };
        }

        private string GetHighestLayerInformation(string key)
        {
            var result = "";
            foreach (var configuration in _configurationLayers)
            {
                if (configuration.Value.TryGetConfiguration(key, out var res))
                    result = res;
                else if (string.IsNullOrEmpty(result))
                    result = res;    
            }

            return result;
        }
    }
}
