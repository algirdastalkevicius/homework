using ConfigurationLibrary.Models;
using System.Collections.Generic;

namespace ConfigurationLibrary.Factories
{
    public interface ILayeredConfigurationFactory
    {
        public IDictionary<int, Configuration> GetLayeredConfiguration();
    }
}
