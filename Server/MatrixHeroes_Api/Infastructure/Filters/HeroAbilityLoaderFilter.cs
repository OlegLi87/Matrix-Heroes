using System;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatrixHeroes_Api.Infastructure.Filters
{
    public class HeroAbilityLoaderFilterAttribute : ActionFilterAttribute
    {
        private IUnitOfWork _unitOfWork;
        public HeroAbilityLoaderFilterAttribute(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var heroInputDto = context.ActionArguments["heroInputDto"] as HeroInputDto;
            var ability = await _unitOfWork.AbilitiesRepository.GetByIdAsync(heroInputDto.AbilityId ?? new Guid());

            if (ability == null)
            {
                context.Result = new ObjectResult(new AppResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ResponsePayload = new ResponsePayload { Errors = new[] { "Ability Id is invalid." } }
                });
                return;
            }
            context.HttpContext.Items["ability"] = ability;
            await next();
        }
    }
}