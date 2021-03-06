﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLibrary.Parsers
{
    public interface IConfigurationParser
    {
        public Task<IDictionary<string, string>> ParseConfigurationAsync(string filename);
    }
}
