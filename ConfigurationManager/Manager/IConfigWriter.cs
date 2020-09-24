using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationManager.Manager
{
    public interface IConfigWriter
    {
        public void AddConfigurationLayer(int id, IDictionary<string, string> layer);
    }
}
