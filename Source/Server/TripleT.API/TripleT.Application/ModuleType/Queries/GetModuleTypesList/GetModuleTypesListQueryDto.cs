using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.ModuleType.Queries.GetModuleTypesList
{
    public class GetModuleTypesListQueryDto : IMapFrom<ModuleTypeEntity>
    {
        public long Id { get; set; }
        public string TypeName { get; set; }
    }
}
