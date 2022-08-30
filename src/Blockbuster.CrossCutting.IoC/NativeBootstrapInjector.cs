using Blockbuster.Application.AppServices;
using Blockbuster.Application.AutoMapper;
using Blockbuster.Application.Interfaces;
using Blockbuster.Domain.Interfaces;
using Blockbuster.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blockbuster.CrossCutting.IoC
{
    public static class NativeBootstrapInjector
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            // Infra 
            services.AddScoped<IMoviesRepository, MoviesRepository>();

            //Application Services
            services.AddScoped<IMoviesAppService, MoviesAppService>();

            var mapper = AutoMapperConfiguration.RegisterMappings().CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}