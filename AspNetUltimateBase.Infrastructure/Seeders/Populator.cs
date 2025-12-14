using AspNetUltimateBase.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AspNetUltimateBase.Infrastructure.Seeders;

public class Populator(
    FoodBarDbContext _dbContext,
    ProductSeeder _productSeeder,
    UserSeeder _userSeeder)
    : IPopulator
{
    public async Task Populate()
    {
        if (_dbContext.Database.CanConnect())
        {
            IEnumerable<string> pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if (pendingMigrations != null && pendingMigrations.Any())
                _dbContext.Database.Migrate();

            await _userSeeder.Seed();
            await _productSeeder.Seed();
        }
    }
}

