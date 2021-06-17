using MatrixHeroes_Api.Core.Repositories;

namespace MatrixHeroes_Api.Core
{
    public interface IUnitOfWork
    {
        IHeroesRepository HeroesRepository { get; set; }
        IAbilitiesRepository AbilitiesRepository { get; set; }
    }
}