using AutoMapper;
using FeirasLivres.Application.Services;
using FeirasLivres.Application.Services.Interfaces;
using FeirasLivres.Core.Interfaces.Repositories;
using FeirasLivres.Infrastructure.Persistence;
using FeirasLivres.Infrastructure.Persistence.Mappers;
using FeirasLivres.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FeirasLivres.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationSqlServer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<FeiraDBContext>(m => m.UseSqlServer(config.GetConnectionString("FeiraDB")), ServiceLifetime.Singleton);
        }

        public static void AddRepositoriesServices(this IServiceCollection services)
        {
            services.AddTransient<IFeiraRepository, FeiraRepository>();
            services.AddTransient<IDistritoRepository, DistritoRepository>();
            services.AddTransient<ISubprefeituraRepository, SubprefeituraRepository>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFeiraServices, FeiraServices>();
            services.AddScoped<IDistritoServices, DistritoServices>();
            services.AddScoped<ISubprefeituraServices, SubprefeituraServices>();
        }

        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(C =>
            {
                C.AddProfile(new AutoMapperProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API - Feiras Livres",
                    Version = "v1",
                    Description = "This API provides information and management of street markets in the city of São Paulo"
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
