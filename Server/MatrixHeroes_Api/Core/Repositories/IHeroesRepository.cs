using System.Collections.Generic;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models.Domain;

namespace MatrixHeroes_Api.Core.Repositories
{
    public interface IHeroesRepository : IRepository<Hero>
    {
        Task<IEnumerable<Hero>> GetHeroes(AppUser user);
        Task<bool> TrainHero(Hero hero);
    }
}