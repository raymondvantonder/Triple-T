using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.ModuleType.Queries.GetModuleTypesList
{
    public class GetModuleTypesListQueryViewModel
    {
        public IEnumerable<GetModuleTypesListQueryDto> ModuleTypes { get; set; }
    }
}
