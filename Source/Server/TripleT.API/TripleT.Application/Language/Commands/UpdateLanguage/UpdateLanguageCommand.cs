using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;
using TripleT.Domain.Exceptions;

namespace TripleT.Application.Language.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest
    {
        public long LanguageId { get; set; }
        public string NewLanguage { get; set; }
    }

    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand>
    {
        private readonly ILogger<UpdateLanguageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public UpdateLanguageCommandHandler(ILogger<UpdateLanguageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            LanguageEntity languageEntity = await _contex.Languages.FindAsync(new object[] { request.LanguageId }, cancellationToken);

            if (languageEntity == null)
            {
                _logger.LogError($"Could not find language with id [{request.LanguageId}]");
                throw new EntityNotFoundException($"Could not find language with id [{request.LanguageId}]");
            }

            if (languageEntity.Value == request.NewLanguage)
            {
                _logger.LogWarning($"New name [{request.NewLanguage}] and existing name [{languageEntity.Value}] is the same, not updating.");
                return Unit.Value;
            }

            languageEntity.Value = request.NewLanguage;

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Language updated successfully");

            return Unit.Value;
        }
    }
}
