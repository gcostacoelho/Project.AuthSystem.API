using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Dtos;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.UserServices;

// * Using a primary constructor, new feature from CSharp
public class UserService(AppDbContext appDbContext) : IUserService
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public Task DeleteUser(Guid identity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(Guid identity)
    {
        throw new NotImplementedException();
    }

    public Task<User> NewUser(UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePassword(Guid identity, string newPass)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(UserDto user, Guid identity)
    {
        throw new NotImplementedException();
    }
}