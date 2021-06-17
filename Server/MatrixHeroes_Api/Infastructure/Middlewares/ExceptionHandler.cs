using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MatrixHeroes_Api.Infastructure.Middlewares
{
    public class ExceptionHandler
    {
        private RequestDelegate _next;
        public ExceptionHandler(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ILogger<ExceptionHandler> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred outside of MVC middleware");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error.");
            }
        }
    }
}