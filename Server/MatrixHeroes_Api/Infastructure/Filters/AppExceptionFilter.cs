using System;
using MatrixHeroes_Api.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class AppExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private ILogger<AppExceptionFilterAttribute> _logger;
        public AppExceptionFilterAttribute(ILogger<AppExceptionFilterAttribute> logger) => _logger = logger;

        public void OnException(ExceptionContext context)
        {
            // logs exception that occurred in action,action filter,during model binding
            _logger.LogError(context.Exception, "exception ocurred.");
            context.Result = new ObjectResult(new AppResponse
            {
                StatusCode = 500,
                ResponsePayload = new ResponsePayload { Errors = new[] { "Server Internal Error." } }
            });
        }
    }
}