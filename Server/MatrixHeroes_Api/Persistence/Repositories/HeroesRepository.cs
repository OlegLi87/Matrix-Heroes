using System.Collections.Generic;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MatrixHeroes_Api.Persistence.Repositories
{
    public class HeroesRepository : Repository<Hero>, IHeroesRepository
    {
        public HeroesRepository(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Hero>> GetHeroes(AppUser user)
        {
            var heroes = user.Heroes;

            foreach (var hero in heroes)
                hero.TryResetCounter();

            await DbContext.SaveChangesAsync();
            return heroes;
        }

        public async Task<bool> TrainHero(Hero hero)
        {
            if (hero.Train())
            {
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}