using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MatrixHeroes_Api.Infastructure.Middlewares
{
    public class RequestResponseLogger
    {
        private RequestDelegate _next;
        public RequestResponseLogger(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ILogger<RequestResponseLogger> logger)
        {
            var requestedUrl = context.Request.Path;
            var username = context.User?.Identity.Name ?? "UnAuthenticated";

            logger.LogInformation("Request arrived for: {url},username: {username}", requestedUrl, username);

            await _next(context);

            var statusCode = context.Response.StatusCode;
            logger.LogInformation("Responded with status code: {statusCode}", statusCode);
        }
    }
}