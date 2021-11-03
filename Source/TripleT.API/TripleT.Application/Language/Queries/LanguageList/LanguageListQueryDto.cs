using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Language.Queries.LanguageList
{
    public class LanguageListQueryDto : IMapFrom<LanguageEntity>
    {
        public long Id { get; set; }
        public string Language { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LanguageEntity, LanguageListQueryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Value));
        }
    }
}
