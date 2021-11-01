using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Module.Queries.ModulesList
{
    public class ModulesListQueryDto : IMapFrom<ModuleEntity>
    {
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }
        public string SubjectName { get; set; }
        public long SubjectId { get; set; }
        public string Grade { get; set; }
        public long GradeId { get; set; }
        public string Language { get; set; }
        public long LanguageId { get; set; }
        public string ModuleType { get; set; }
        public long ModuleTypeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModuleEntity, ModulesListQueryDto>()
                .ForMember(x => x.ModuleId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.FileLocation, opt => opt.MapFrom(x => x.FileLocation))
                .ForMember(x => x.SubjectName, opt => opt.MapFrom(x => x.Subject.Name))
                .ForMember(x => x.SubjectId, opt => opt.MapFrom(x => x.SubjectId))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.Grade.Value))
                .ForMember(x => x.GradeId, opt => opt.MapFrom(x => x.GradeId))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language.Value))
                .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.LanguageId))
                .ForMember(x => x.ModuleType, opt => opt.MapFrom(x => x.ModuleType.TypeName))
                .ForMember(x => x.ModuleTypeId, opt => opt.MapFrom(x => x.ModuleTypeId));
        }
    }
}
