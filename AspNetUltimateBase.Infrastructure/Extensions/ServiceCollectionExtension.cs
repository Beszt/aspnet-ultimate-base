using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspNetUltimateBase.Domain.Interfaces;
using AspNetUltimateBase.Application.Interfaces;
using AspNetUltimateBase.Infrastructure.Persistence;
using AspNetUltimateBase.Infrastructure.Repositories;
using AspNetUltimateBase.Infrastructure.Seeders;
using AspNetUltimateBase.Infrastructure.Services;

namespace AspNetUltimateBase.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FoodBarDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("AspNetUltimateBase")
        ));

        services.AddScoped<ProductSeeder>();
        services.AddScoped<UserSeeder>();
        services.AddScoped<IPopulator, Populator>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDatabaseHealthChecker, DatabaseHealthChecker>();
    }
}
