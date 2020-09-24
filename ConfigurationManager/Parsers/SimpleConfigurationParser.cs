using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConfigurationManager.Helpers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfigurationManager.Parsers
{
    public class SimpleConfigurationParser : IConfigurationParser
    {
        private const string Pattern = @"(?<key>.*):\t+(?<value>.*)";

        public async Task<IDictionary<string, string>> ParseConfigurationAsync(string filename)
        {
            var result = new Dictionary<string, string>();
            var regex = new Regex(Pattern);

            using var reader = new StreamReader(new FileStream(filename, FileMode.Open));
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                line = line.RemoveComments().Trim().Trim('\t');

                var match = regex.Match(line);
                if(match.Success)
                {
                    var index = match.Value.IndexOf(':');

                    var key = match.Groups["key"].Value;
                    var value = match.Groups["value"].Value;
                    result.Add(key, value);
                }
            }

            return result;
        }
    }
}
