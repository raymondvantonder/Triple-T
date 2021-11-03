using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Common.Authorization.Models
{
    public class TokenDetails
    {
        public string Token { get; set; }
        public int ExpiresInSeconds { get; set; }
    }
}
