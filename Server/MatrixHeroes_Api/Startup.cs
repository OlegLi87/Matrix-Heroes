using MatrixHeroes_Api.Core;
using MatrixHeroes_Api.Core.Services;
using MatrixHeroes_Api.Infastructure;
using MatrixHeroes_Api.Infastructure.ExtensionMethods;
using MatrixHeroes_Api.Infastructure.Filters;
using MatrixHeroes_Api.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace MatrixHeroes_Api
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config) => _config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_config);

            services.ConfigureDbContext(_config);
            services.ConfigureIdentity();
            services.ConfigureAuthentication();
            services.ConfigureCors();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AppUserLoaderFilterAttribute>();
            services.AddScoped<HeroAbilityLoaderFilterAttribute>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(AppResultFilterAttribute));
                opts.Filters.Add(typeof(AppExceptionFilterAttribute));

                opts.RespectBrowserAcceptHeader = true;
                opts.ReturnHttpNotAcceptable = true;
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettings> opts)
        {
            var appSettings = opts.Value;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(appSettings.Cors.PolicyName);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment() || (_config["Seed"] ?? "") == "true")
                SeedDb.Seed(app.ApplicationServices);
        }
    }
}
