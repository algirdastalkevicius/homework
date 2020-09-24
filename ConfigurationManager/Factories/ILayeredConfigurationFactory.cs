using ConfigurationManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationManager.Factories
{
    public interface ILayeredConfigurationFactory
    {
        public IDictionary<int, Configuration> GetLayeredConfiguration();
    }
}
