using System;

namespace TripleT.User.Infrastructure.Persistence.Models
{
    public abstract class DocumentBase<TParentDocument>
        where TParentDocument : DocumentBase<TParentDocument>
    {
        public virtual string PK { get; set; }
    
        public virtual string SK { get; set; }
    
        public DateTime CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }

        public string Type { get; set; } = typeof(TParentDocument).Name;
    }
}