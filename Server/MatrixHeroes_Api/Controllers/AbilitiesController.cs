using System.Linq;
using AutoMapper;
using MatrixHeroes_Api.Core;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatrixHeroes_Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Authorize(Roles = "Admin,Trainer")]
    public class AbilitiesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _autoMapper;
        public AbilitiesController(IUnitOfWork unitOfWork, IMapper autoMapper)
             => (_unitOfWork, _autoMapper) = (unitOfWork, autoMapper);

        [HttpGet]
        public AppResponse GetAbilities()
        {
            var abilities = _unitOfWork.AbilitiesRepository.Get();
            return new AppResponse
            {
                StatusCode = 200,
                ResponsePayload = new ResponsePayload { ResponseObj = abilities.Select(_autoMapper.Map<AbilityDto>) }
            };
        }
    }
}