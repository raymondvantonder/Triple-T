using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Module.Commands.CreateModule
{
    public class CreateModuleCommand : IRequest<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FileLocation { get; set; }
        public long SubjectId { get; set; }
        public long LanguageId { get; set; }
        public long ModuleTypeId { get; set; }
        public long GradeId { get; set; }
    }

    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, long>
    {
        private readonly ILogger<CreateModuleCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateModuleCommandHandler(ILogger<CreateModuleCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            ModuleEntity existingSummary = await _contex.Modules.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (existingSummary != null)
            {
                _logger.LogError($"Summary with name [{request.Name}] already exists.");
                throw new DuplicateEntityException($"Summary with name [{request.Name}] already exists.");
            }

            var summaryEntity = new ModuleEntity
            {
                FileLocation = request.FileLocation,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                SubjectId = request.SubjectId,
                LanguageId = request.LanguageId,
                ModuleTypeId = request.ModuleTypeId,
                GradeId = request.GradeId
            };

            //Check if file exists in s3 bucket

            await _contex.Modules.AddAsync(summaryEntity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Successfully created summary with id [{summaryEntity.Id}].");

            return summaryEntity.Id;
        }
    }
}
