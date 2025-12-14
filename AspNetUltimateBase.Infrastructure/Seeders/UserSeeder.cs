using Microsoft.AspNetCore.Identity;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Infrastructure.Persistence;

namespace AspNetUltimateBase.Infrastructure.Seeders;

public class UserSeeder(
    FoodBarDbContext _dbContext)
    : ISeeder
{
    public async Task Seed()
    {
        if (!_dbContext.Roles.Any())
        {
            List<RoleEntity> roles = [
                new() { Name = "admin" },
                new() { Name = "user" }
            ];

            PasswordHasher<UserEntity> passwordHasher = new();

            await _dbContext.Roles.AddRangeAsync(roles);
            await _dbContext.SaveChangesAsync();

            int adminRoleId = roles.First(r => r.Name == "admin").Id;
            int userRoleId = roles.First(r => r.Name == "user").Id;

            UserEntity admin = new()
            {
                Login = "admin",
                RoleId = adminRoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            admin.Password = passwordHasher.HashPassword(admin, "admin"); // TP-Link tribute

            UserEntity user = new()
            {
                Login = "user",
                RoleId = userRoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            user.Password = passwordHasher.HashPassword(user, "12345");

            await _dbContext.Users.AddAsync(admin);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}

