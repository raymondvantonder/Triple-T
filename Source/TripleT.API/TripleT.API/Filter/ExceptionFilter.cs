using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleT.API.Filter.Models;
using TripleT.Application.Common.Exceptions;
using TripleT.Application.Common.Extensions;
using TripleT.Application.Common.Models.Enumerations;
using TripleT.Domain.Exceptions;

namespace TripleT.API.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private Dictionary<Type, Action<ExceptionContext>> _exceptions;

        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            _exceptions = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(AuthenticationFailedException), AuthenticationFailedExceptionHandler},
                { typeof(ValidationException), ValidationExceptionHandler},
                { typeof(EntityNotFoundException), EntityNotFoundExceptionHandler},
                { typeof(DuplicateEntityException), DuplicateEntityExceptionHandler},
            };

            if (_exceptions.TryGetValue(context.Exception.GetType(), out Action<ExceptionContext> exceptionHandler))
            {
                exceptionHandler(context);
                return;
            }

            UnknownExceptionHandler(context);
        }

        private void DuplicateEntityExceptionHandler(ExceptionContext context)
        {
            var errorResponse = new ErrorResponse
            {
                ErrorCode = ApplicationErrorCodes.DUPLICATE_ENTITY.GetEnumCode(),
                Message = context.Exception.Message
            };

            _logger.LogError($"Duplicate entity. Message: [{context.Exception.Message}]");

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status409Conflict
            };
        }

        private void EntityNotFoundExceptionHandler(ExceptionContext context)
        {
            var errorResponse = new ErrorResponse
            {
                ErrorCode = ApplicationErrorCodes.ENTITY_NOT_FOUND.GetEnumCode(),
                Message = context.Exception.Message
            };

            _logger.LogError($"Entity not found. Message: [{context.Exception.Message}]");

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        private void ValidationExceptionHandler(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            _logger.LogWarning($"Validation failed: [{exception.Errors?.FormatAsJsonForLogging()}]");

            var errorResponse = new DetailedErrorResponse
            {
                ErrorCode = ApplicationErrorCodes.INVALID_INPUT.GetEnumCode(),
                Message = "Input validation failed",
                Details = exception.Errors.Select(x => $"{x.Key}: {string.Join(", ", x.Value)}").ToList()
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        private void AuthenticationFailedExceptionHandler(ExceptionContext context)
        {
            _logger.LogWarning($"Access forbidden to endpoint: [{context.Exception.Message}]");

            context.Result = new ForbidResult();
        }

        private void UnknownExceptionHandler(ExceptionContext context)
        {
            _logger.LogError($"Unhandled error occurred: [{context.Exception}]");

            var errorResponse = new ErrorResponse
            {
                ErrorCode = ApplicationErrorCodes.UNHANDLED_ERROR.GetEnumCode(),
                Message = context.Exception.Message
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
