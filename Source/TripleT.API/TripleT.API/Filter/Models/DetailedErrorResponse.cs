using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripleT.API.Filter.Models
{
    public class DetailedErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public List<string> Details { get; set; } = new List<string>();
    }
}
