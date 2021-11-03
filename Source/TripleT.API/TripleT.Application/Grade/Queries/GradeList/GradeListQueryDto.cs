using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Grade.Queries.GradeList
{
    public class GradeListQueryDto : IMapFrom<GradeEntity>
    {
        public long Id { get; set; }
        public string Grade { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GradeEntity, GradeListQueryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.Value));
        }
    }
}
