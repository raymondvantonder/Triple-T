using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TripleT.Application.Common.Extensions;

namespace TripleT.Application.Common.Behaviours
{
    public class RequestLoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestLoggerBehaviour<TRequest, TResponse>> _logger;

        public RequestLoggerBehaviour(ILogger<RequestLoggerBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug(request?.FormatAsJsonForLogging());

            return next();
        }
    }
}
