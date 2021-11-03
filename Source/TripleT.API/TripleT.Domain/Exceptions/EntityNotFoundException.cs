using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
