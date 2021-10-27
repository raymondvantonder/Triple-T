using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleT.API.Filter.Models
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
