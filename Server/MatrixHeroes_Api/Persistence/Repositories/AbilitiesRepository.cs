using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MatrixHeroes_Api.Persistence.Repositories
{
    public class AbilitiesRepository : Repository<Ability>, IAbilitiesRepository
    {
        public AbilitiesRepository(DbContext context) : base(context)
        { }
    }
}