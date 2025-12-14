using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Infrastructure.Persistence;
using AspNetUltimateBase.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetUltimateBase.Infrastructure.Tests.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task Get_ReturnsUserWithRole()
    {
        await using FoodBarDbContext context = CreateContext();
        RoleEntity adminRole = new() { Id = 1, Name = "admin" };
        context.Roles.Add(adminRole);
        context.Users.Add(new UserEntity { Id = 1, Login = "admin", Password = "pwd", RoleId = adminRole.Id, Role = adminRole });
        await context.SaveChangesAsync();

        UserRepository repository = new(context);

        UserEntity? user = await repository.Get("admin");

        user.Should().NotBeNull();
        user!.Role.Should().NotBeNull();
        user.Role.Name.Should().Be("admin");
    }

    [Fact]
    public void HasAdminRole_ReturnsTrueOnlyForAdmin()
    {
        using FoodBarDbContext context = CreateContext();
        RoleEntity adminRole = new() { Id = 1, Name = "admin" };
        RoleEntity userRole = new() { Id = 2, Name = "user" };
        context.Roles.AddRange(adminRole, userRole);
        context.Users.AddRange(
            new UserEntity { Id = 1, Login = "admin", Password = "pwd", RoleId = adminRole.Id, Role = adminRole },
            new UserEntity { Id = 2, Login = "bob", Password = "pwd", RoleId = userRole.Id, Role = userRole }
        );
        context.SaveChanges();

        UserRepository repository = new(context);

        repository.HasAdminRole(1).Should().BeTrue();
        repository.HasAdminRole(2).Should().BeFalse();
    }

    [Fact]
    public async Task Delete_RemovesUser()
    {
        await using FoodBarDbContext context = CreateContext();
        RoleEntity userRole = new() { Id = 2, Name = "user" };
        context.Roles.Add(userRole);
        context.Users.Add(new UserEntity { Id = 1, Login = "bob", Password = "pwd", RoleId = userRole.Id, Role = userRole });
        await context.SaveChangesAsync();

        UserRepository repository = new(context);
        await repository.Delete("bob");

        context.Users.Any(u => u.Login == "bob").Should().BeFalse();
    }

    [Fact]
    public void GetRoleIdByRoleName_ReturnsZeroWhenMissing()
    {
        using FoodBarDbContext context = CreateContext();
        context.Roles.Add(new RoleEntity { Id = 1, Name = "admin" });
        context.SaveChanges();

        UserRepository repository = new(context);

        repository.GetRoleIdByRoleName("ghost").Should().Be(0);
        repository.GetRoleIdByRoleName("admin").Should().Be(1);
    }

    private static FoodBarDbContext CreateContext()
    {
        DbContextOptions<FoodBarDbContext> options = new DbContextOptionsBuilder<FoodBarDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new FoodBarDbContext(options);
    }
}
