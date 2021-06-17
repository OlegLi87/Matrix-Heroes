using System;
using System.Linq;
using MatrixHeroes_Api.Core.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MatrixHeroes_Api.Persistence
{
    public static class SeedDb
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            var scopedProvider = serviceProvider.CreateScope().ServiceProvider;

            var dbContext = scopedProvider.GetRequiredService<MatrixHeroesDbContext>();
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();

            if (!roleManager.Roles.Any())
            {
                var roleNames = new string[] { "Admin", "Trainer", "Wathcer" };
                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            if (!dbContext.Abilities.Any())
            {
                var abilityNames = new string[] { "Attacker", "Defenser", "Reviver", "Mage" };
                foreach (var abilityName in abilityNames)
                {
                    var ability = new Ability { Name = abilityName };
                    dbContext.Abilities.Add(ability);
                }
                dbContext.SaveChanges();
            }
        }
    }
}