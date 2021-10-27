using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.SummaryCategory.Queries.SummaryCategoryList
{
    public class SummaryCategoryListQueryDto : IMapFrom<SummaryCategoryEntity>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SummaryCategoryEntity, SummaryCategoryListQueryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name));
        }
    }
}
