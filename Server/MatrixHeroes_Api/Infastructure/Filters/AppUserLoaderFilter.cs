using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class AppUserLoaderFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        private UserManager<AppUser> _userManager;
        public AppUserLoaderFilterAttribute(UserManager<AppUser> userManager) => _userManager = userManager;

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);
            if (user == null)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ResponsePayload = new ResponsePayload { Errors = new[] { "Token not valid." } }
                });
                return;
            }
            context.HttpContext.Items["user"] = user;

            await next();
        }
    }
}