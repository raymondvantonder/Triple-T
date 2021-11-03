using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TripleT.Domain.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException()
        {
        }

        public DuplicateEntityException(string message) : base(message)
        {
        }

        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
