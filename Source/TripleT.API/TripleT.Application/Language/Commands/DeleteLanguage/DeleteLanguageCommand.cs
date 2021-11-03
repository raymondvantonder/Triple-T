using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Domain.Entities;

namespace TripleT.Application.Language.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest
    {
        public long LanguageId { get; set; }
    }

    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
    {
        private readonly ILogger<DeleteLanguageCommandHandler> _logger;
        private readonly ITripleTDbContext _contex;

        public DeleteLanguageCommandHandler(ILogger<DeleteLanguageCommandHandler> logger, ITripleTDbContext context)
        {
            _logger = logger;
            _contex = context;
        }

        public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            LanguageEntity languageEntity = await _contex.Languages.FindAsync(new object[] { request.LanguageId }, cancellationToken);

            if (languageEntity == null)
            {
                _logger.LogWarning($"Language [{request.LanguageId}] not found for deleting.");
                return Unit.Value;
            }

            _contex.Languages.Remove(languageEntity);

            await _contex.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Language deleted: [{languageEntity.FormatAsJsonForLogging()}]");

            return Unit.Value;
        }
    }
}
