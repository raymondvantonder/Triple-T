using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Package.Queries.GetPackageList
{
    public class GetPackageListQueryValidator : AbstractValidator<GetPackageListQueryDto>
    {
        public GetPackageListQueryValidator()
        {
        }
    }
}
