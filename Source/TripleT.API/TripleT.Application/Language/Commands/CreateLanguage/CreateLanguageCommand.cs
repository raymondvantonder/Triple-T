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

namespace TripleT.Application.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommand : IRequest<long>
    {
        public string Language { get; set; }
    }

    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, long>
    {
        private readonly ILogger<CreateLanguageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public CreateLanguageCommandHandler(ILogger<CreateLanguageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<long> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            LanguageEntity existingLanguageEntity = await _contex.Languages.FirstOrDefaultAsync(x => x.Value == request.Language, cancellationToken);

            if (existingLanguageEntity != null)
            {
                _logger.LogError($"Language [{request.Language}] already exists");
                throw new DuplicateEntityException($"Language [{request.Language}] already exists");
            }

            var entity = new LanguageEntity { Value = request.Language };

            await _contex.Languages.AddAsync(entity, cancellationToken);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Added Language [{request.Language}] successfully");

            return entity.Id;
        }
    }
}
