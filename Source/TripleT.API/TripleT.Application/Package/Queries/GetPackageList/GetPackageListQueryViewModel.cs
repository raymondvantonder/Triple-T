using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Package.Queries.GetPackageList
{
    public class GetPackageListQueryViewModel
    {
        public IEnumerable<GetPackageListQueryDto> Packages { get; set; }
    }
}
