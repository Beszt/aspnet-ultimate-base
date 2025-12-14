using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Domain.Interfaces;

public interface IUserRepository
{
    Task Create(UserEntity user);
    Task<UserEntity> Get(string login);
    Task Update(UserEntity user);
    Task Delete(string login);

    bool Exists(string login);
    int GetRoleIdByRoleName(string name);
    string GetRoleName(string login);
    bool HasAdminRole(int id);
}

