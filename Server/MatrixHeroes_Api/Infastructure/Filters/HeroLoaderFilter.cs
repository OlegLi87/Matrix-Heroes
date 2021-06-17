using System;
using System.Linq;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class HeroLoaderFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Items["user"] as AppUser;
            var heroId = (Guid)context.ActionArguments["id"];

            var hero = user.Heroes.FirstOrDefault(h => h.Id == heroId);
            if (hero == null)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ResponsePayload = new ResponsePayload { Errors = new[] { "User doesnt have a hero with the given Id." } }
                });
                return;
            }
            context.HttpContext.Items["hero"] = hero;
        }
    }
}