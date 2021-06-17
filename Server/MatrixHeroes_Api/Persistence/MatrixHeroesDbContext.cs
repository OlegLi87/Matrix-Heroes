using System;
using System.Linq;
using MatrixHeroes_Api.Core.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatrixHeroes_Api.Persistence
{
    public class MatrixHeroesDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Ability> Abilities { get; set; }

        public MatrixHeroesDbContext(DbContextOptions<MatrixHeroesDbContext> opts) : base(opts)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Hero>()
                   .Property(h => h.SuitColors)
                   .HasConversion(
                       s => string.Join(';', s),
                       s => s.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet()
                       );

            base.OnModelCreating(builder);
        }
    }
}