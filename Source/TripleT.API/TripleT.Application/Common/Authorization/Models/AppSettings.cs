using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Common.Authorization.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
