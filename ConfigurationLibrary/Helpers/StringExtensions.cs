using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationLibrary.Helpers
{
    public static class StringExtensions
    {
        public static string RemoveComments(this string value)
        {
            if (value.Contains(@"//"))
            {
                var index = value.IndexOf(@"//");
                value = value.Substring(0, index);
            }

            return value;
        }
    }
}
