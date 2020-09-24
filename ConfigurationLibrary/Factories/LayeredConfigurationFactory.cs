using ConfigurationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationLibrary.Factories
{
    public class LayeredConfigurationFactory : ILayeredConfigurationFactory
    {
        private IDictionary<int, Configuration> _layeredConfiguration;

        public IDictionary<int, Configuration> GetLayeredConfiguration()
        {
            if (_layeredConfiguration == null)
                _layeredConfiguration = new Dictionary<int, Configuration>();

            return _layeredConfiguration;
        }
    }
}
