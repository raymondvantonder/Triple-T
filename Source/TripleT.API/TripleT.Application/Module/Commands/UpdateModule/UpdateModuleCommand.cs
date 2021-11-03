using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Module.Commands.UpdateModule
{
    public class UpdateModuleCommand : IRequest
    {
        public long ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string FileLocation { get; set; }
        public long? SubjectId { get; set; }
        public long? LanguageId { get; set; }
        public long? ModuleTypeId { get; set; }
        public long? GradeId { get; set; }
    }

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
    {
        private readonly ILogger<UpdateModuleCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateModuleCommandHandler(ILogger<UpdateModuleCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            ModuleEntity moduleEntity = await _contex.Modules.MustFindAsync(request.ModuleId, cancellationToken);

            moduleEntity.Name = request.Name ?? moduleEntity.Name;
            moduleEntity.Description = request.Description ?? moduleEntity.Description;
            moduleEntity.Price = request.Price ?? moduleEntity.Price;
            moduleEntity.FileLocation = request.FileLocation ?? moduleEntity.FileLocation;
            
            moduleEntity.SubjectId = request.SubjectId ?? moduleEntity.SubjectId;
            moduleEntity.LanguageId = request.LanguageId ?? moduleEntity.LanguageId;
            moduleEntity.GradeId = request.GradeId ?? moduleEntity.GradeId;
            moduleEntity.ModuleTypeId = request.ModuleTypeId ?? moduleEntity.ModuleTypeId;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Updated summary successfully");

            return Unit.Value;
        }
    }
}
