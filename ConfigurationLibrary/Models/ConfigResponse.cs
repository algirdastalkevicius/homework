using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationLibrary.Models
{
    public class ConfigResponse<T>
    {
        public T Value { get; set; }
        public IList<string> Errors { get; private set; }
        public bool Succeeded { get; set; }

        public ConfigResponse()
        {
            Errors = new List<string>();
        }
    }
}
