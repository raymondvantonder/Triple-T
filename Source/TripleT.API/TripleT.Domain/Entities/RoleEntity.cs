using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Domain.Entities.Common;

namespace TripleT.Domain.Entities
{
    public class RoleEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
