using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Subject.Queries.SubjectList
{
    public class SubjectListQueryDto : IMapFrom<SubjectEntity>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubjectEntity, SubjectListQueryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name));
        }
    }
}
