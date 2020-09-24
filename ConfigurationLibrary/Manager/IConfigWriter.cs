using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationLibrary.Manager
{
    public interface IConfigWriter
    {
        public void AddConfigurationLayer(int id, IDictionary<string, string> layer);
    }
}
