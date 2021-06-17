using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class SignUpActionFilterAttribute : ActionFilterAttribute
    {
        public string ActionArgumentName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var credentials = context.ActionArguments[ActionArgumentName] as UserCredentials;
            if (credentials.Email == null)
            {
                var appResponse = new AppResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ResponsePayload = new ResponsePayload { Errors = new[] { "Email address must be provided when signing up." } }
                };
                context.Result = new ObjectResult(appResponse);
            }
        }
    }
}