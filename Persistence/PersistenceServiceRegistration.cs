using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersitenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<BaseDbContext>(options =>{options.UseInMemoryDatabase("nArchitecture");});

            services.AddDbContext<BaseDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("MyCon")); });

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IFuelRepository, FuelRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<ITransmissionRepository, TransmissionRepository>();

            return services;
        }
    }
}
