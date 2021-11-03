using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.Package.Queries.GetPackageList
{
    public class GetPackageListQueryDto : IMapFrom<PackageEntity>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long SubjectId { get; set; }
        public string Subject { get; set; }
        public long GradeId { get; set; }
        public string Grade { get; set; }
        public List<GetPackageListModuleDto> Modules { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PackageEntity, GetPackageListQueryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.SubjectId, opt => opt.MapFrom(x => x.SubjectId))
                .ForMember(x => x.Subject, opt => opt.MapFrom(x => x.Subject.Name))
                .ForMember(x => x.GradeId, opt => opt.MapFrom(x => x.GradeId))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.Grade.Value));
        }
    }

    public class GetPackageListModuleDto : IMapFrom<ModuleEntity>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public long SubjectId { get; set; }
        public string Subject { get; set; }
        public long GradeId { get; set; }
        public string Grade { get; set; }
        public long ModuleTypeId { get; set; }
        public string ModuleType { get; set; }
        public long LanguageId { get; set; }
        public string Language { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModuleEntity, GetPackageListModuleDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.SubjectId, opt => opt.MapFrom(x => x.SubjectId))
                .ForMember(x => x.Subject, opt => opt.MapFrom(x => x.Subject.Name))
                .ForMember(x => x.GradeId, opt => opt.MapFrom(x => x.GradeId))
                .ForMember(x => x.Grade, opt => opt.MapFrom(x => x.Grade.Value))
                .ForMember(x => x.ModuleTypeId, opt => opt.MapFrom(x => x.ModuleTypeId))
                .ForMember(x => x.ModuleType, opt => opt.MapFrom(x => x.ModuleType.TypeName))
                .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.LanguageId))
                .ForMember(x => x.Language, opt => opt.MapFrom(x => x.Language.Value));
        }
    }

}
