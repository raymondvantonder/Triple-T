using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Common.Extensions
{
    public static class ObjectLoggingExtensions
    {
        public static string FormatAsJsonForLogging(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(value);
        }
    }
}
