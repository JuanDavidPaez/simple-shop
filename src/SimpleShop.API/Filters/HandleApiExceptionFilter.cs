using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleShop.API.Filters
{
    class HandleApiExceptionFilter : IExceptionFilter
    {
        private readonly IDictionary<Type, Func<ExceptionContext, ErrorResponse>> exceptionHandlers;
        private readonly IHostEnvironment hostEnvironment;
        private readonly ILogger<HandleApiExceptionFilter> logger;
        public HandleApiExceptionFilter(IHostEnvironment hostEnvironment,
            ILogger<HandleApiExceptionFilter> logger)
        {
            this.hostEnvironment = hostEnvironment;
            this.logger = logger;

            exceptionHandlers = GetExceptionTypeHandlers();
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, "An unhandled error occurred");

            var errorResponse = HandleException(context);
            if (hostEnvironment.IsDevelopment())
            {
                errorResponse.Ex = context.Exception;
            }
            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = errorResponse?.StatusCode
            };
            context.ExceptionHandled = true;
        }

        private Dictionary<Type, Func<ExceptionContext, ErrorResponse>> GetExceptionTypeHandlers()
        {
            return new Dictionary<Type, Func<ExceptionContext, ErrorResponse>>
            {
                {typeof(BusinessRuleValidationException), HandleBusinessRuleValidationException }
            };
        }

        private ErrorResponse HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                return exceptionHandlers[type].Invoke(context);
            }
            return HandleUnknownException(context);
        }

        private ErrorResponse HandleBusinessRuleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as BusinessRuleValidationException;

            var response = new ErrorResponse(
                StatusCodes.Status400BadRequest,
                exception.Id,
                exception.Message,
                null);
            return response;
        }

        private ErrorResponse HandleUnknownException(ExceptionContext context)
        {
            var response = new ErrorResponse(
                StatusCodes.Status500InternalServerError,
                null,
                "An error occurred while processing your request.",
                null);

            return response;
        }

        class ErrorResponse
        {
            public int StatusCode { get; private set; }
            public string ErrorCode { get; private set; }
            public string Message { get; private set; }
            public string Help { get; private set; }
            /// <summary>
            /// Only used in development
            /// </summary>
            public Exception Ex { get; set; }

            public ErrorResponse(int statusCode, string errorCode, string message, string help)
            {
                StatusCode = statusCode;
                ErrorCode = errorCode;
                Message = message;
                Help = help;
            }
        }
    }
}
