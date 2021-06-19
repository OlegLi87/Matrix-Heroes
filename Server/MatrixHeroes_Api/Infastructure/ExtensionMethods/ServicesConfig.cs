using System;
using System.Text;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MatrixHeroes_Api.Infastructure.ExtensionMethods
{
    public static class ServicesConfig
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MatrixHeroesDb");

            services.AddDbContext<MatrixHeroesDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
                opts.EnableSensitiveDataLogging();
                opts.UseLazyLoadingProxies();
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = false;

                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MatrixHeroesDbContext>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var appSettings = getAppSettings(services);
            var jwtSecret = appSettings.JwtSecret;

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            var appSettings = getAppSettings(services);

            services.AddCors(opts =>
            {
                opts.AddPolicy(appSettings.Cors.PolicyName, builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    foreach (var host in appSettings.Cors.AllowedHosts)
                        builder.WithOrigins(host);
                });
            });
        }

        private static AppSettings getAppSettings(IServiceCollection services) =>
                services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>().Value;
    }
}