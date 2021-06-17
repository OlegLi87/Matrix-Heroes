using MatrixHeroes_Api.Core;
using MatrixHeroes_Api.Core.Repositories;
using MatrixHeroes_Api.Persistence.Repositories;

namespace MatrixHeroes_Api.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IHeroesRepository HeroesRepository { get; set; }
        public IAbilitiesRepository AbilitiesRepository { get; set; }

        public UnitOfWork(MatrixHeroesDbContext dbContext)
        {
            HeroesRepository = new HeroesRepository(dbContext);
            AbilitiesRepository = new AbilitiesRepository(dbContext);
        }
    }
}