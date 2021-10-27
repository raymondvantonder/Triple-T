using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Summary.Queries.SummariesList
{
    public class SummariesListQueryDto : IMapFrom<SummaryEntity>
    {
        public long SummaryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SummaryEntity, SummariesListQueryDto>()
                .ForMember(x => x.SummaryId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.FileLocation, opt => opt.MapFrom(x => x.FileLocation))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.SummaryCategory.Name))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.SummaryCategoryId));
        }
    }
}
