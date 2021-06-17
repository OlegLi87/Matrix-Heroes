using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatrixHeroes_Api.Core;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Models.Dtos;
using MatrixHeroes_Api.Infastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatrixHeroes_Api.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    [Authorize(Roles = "Admin,Trainer")]
    [ServiceFilter(typeof(AppUserLoaderFilterAttribute))]
    public class HeroesController : ControllerBase
    {
        private IMapper _autoMapper;
        private IUnitOfWork _unitOfWork;

        public HeroesController(IMapper autoMapper, IUnitOfWork unitOfWork)
             => (_autoMapper, _unitOfWork) = (autoMapper, unitOfWork);

        [HttpGet]
        public async Task<AppResponse> GetHeroes()
        {
            var user = HttpContext.Items["user"] as AppUser;
            var heroes = await _unitOfWork.HeroesRepository.GetHeroes(user);
            return new AppResponse
            {
                StatusCode = 200,
                ResponsePayload = new ResponsePayload
                {
                    ResponseObj = heroes.Select(_autoMapper.Map<HeroOutputDto>)
                }
            };
        }

        [HttpPost]
        [ServiceFilter(typeof(HeroAbilityLoaderFilterAttribute))]
        public async Task<AppResponse> CreateHero([FromBody] HeroInputDto heroInputDto)
        {
            var user = HttpContext.Items["user"] as AppUser;
            var ability = HttpContext.Items["ability"] as Ability;

            var hero = _autoMapper.Map<Hero>(heroInputDto);
            hero.AppUser = user;
            hero.Ability = ability;
            await _unitOfWork.HeroesRepository.AddAsync(hero);

            var heroOutputDto = _autoMapper.Map<HeroOutputDto>(hero);

            return new AppResponse
            {
                StatusCode = StatusCodes.Status201Created,
                ResponsePayload = new ResponsePayload { ResponseObj = heroOutputDto }
            };
        }

        [HttpPatch]
        [HeroLoaderFilter]
        [Route("{id}")]
        public async Task<AppResponse> TrainHero([FromRoute] Guid id)
        {
            var hero = HttpContext.Items["hero"] as Hero;
            if (await _unitOfWork.HeroesRepository.TrainHero(hero))
                return new AppResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    ResponsePayload = new ResponsePayload { ResponseObj = _autoMapper.Map<HeroOutputDto>(hero) }
                };

            return new AppResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ResponsePayload = new ResponsePayload { Errors = new[] { "Trainings limit reached." } }
            };
        }

        [HttpDelete]
        [HeroLoaderFilter]
        [Route("{id}")]
        public async Task<AppResponse> DeleteHero([FromRoute] Guid id)
        {
            var hero = HttpContext.Items["hero"] as Hero;
            await _unitOfWork.HeroesRepository.RemoveAsync(hero);

            return new AppResponse
            {
                StatusCode = 204
            };
        }
    }
}