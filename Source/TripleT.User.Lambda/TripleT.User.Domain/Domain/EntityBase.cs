using System;

namespace TripleT.User.Domain.Domain
{
    public abstract class EntityBase
    {
        public DateTime CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}