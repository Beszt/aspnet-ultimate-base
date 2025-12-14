using Microsoft.EntityFrameworkCore;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using AspNetUltimateBase.Infrastructure.Persistence;

namespace AspNetUltimateBase.Infrastructure.Repositories;

public class UserRepository(
    FoodBarDbContext _dbContext)
    : IUserRepository
{
    public async Task Create(UserEntity user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserEntity> Get(string login)
    {
        UserEntity user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user != null)
        {
            RoleEntity role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            if (role != null)
                user.Role = role;
        }

        return user;
    }

    public async Task Update(UserEntity user)
    {
        UserEntity usr = _dbContext.Users.FirstOrDefault(p => p.Login == user.Login);

        if (usr != null)
        {
            usr.Password = user.Password;
            usr.RoleId = user.RoleId;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(string login)
    {
        UserEntity user = _dbContext.Users.FirstOrDefault(p => p.Login == login);

        if (user != null)
            _dbContext.Remove(user);

        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(string login) =>
        _dbContext.Users.FirstOrDefault(p => p.Login == login) != null;

    public int GetRoleIdByRoleName(string name)
    {
        RoleEntity role = _dbContext.Roles.FirstOrDefault(r => r.Name == name);

        if (role != null)
            return role.Id;
        else
            return 0;
    }

    public bool HasAdminRole(int id)
    {
        UserEntity user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

        RoleEntity adminRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "admin");

        if (user != null)
            return user.RoleId == adminRole!.Id;
        else
            return false;
    }

    public string GetRoleName(string login)
    {
        UserEntity user = _dbContext.Users.FirstOrDefault(u => u.Login == login);

        if (user != null)
        {
            RoleEntity role = _dbContext.Roles.FirstOrDefault(r => r.Id == user.RoleId)!;

            if (role != null)
            {
                return role.Name;
            }
        }

        return null;
    }
}

