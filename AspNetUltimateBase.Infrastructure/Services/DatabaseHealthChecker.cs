using AspNetUltimateBase.Application.Interfaces;
using AspNetUltimateBase.Infrastructure.Persistence;

namespace AspNetUltimateBase.Infrastructure.Services;

public class DatabaseHealthChecker(FoodBarDbContext _dbContext) : IDatabaseHealthChecker
{
    public async Task<bool> CanConnectAsync()
    {
        return await _dbContext.Database.CanConnectAsync();
    }
}
