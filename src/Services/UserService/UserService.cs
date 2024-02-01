using System.Net;
using Microsoft.EntityFrameworkCore;
using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.UserService;
public class UserService(AppDbContext appDbContext) : IUserService
{
    private readonly AppDbContext _appDbContext = appDbContext;


    public async Task<User> GetUserAsync(Guid identity)
    {
        var user = await _appDbContext.Users.FindAsync(identity);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        return user;
    }

    public async Task<User> NewUserAsync(UserDto user)
    {
        var userComp = new User(user.Email, user.Password, user.Fullname, user.Birthday);

        _appDbContext.Users.Add(userComp);

        await _appDbContext.SaveChangesAsync();

        return userComp;
    }

    public async Task<User> UpdateUserAsync(UserDto user, Guid identity)
    {
        var userInfos = await _appDbContext.Users.FindAsync(identity);

        if (userInfos == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        // ! Search a better way to do this without generate error in sql server;

        userInfos.Email = user.Email;
        userInfos.Password = user.Password;
        userInfos.Fullname = user.Fullname;
        userInfos.Birthday = user.Birthday;

        _appDbContext.Entry(userInfos).State = EntityState.Modified;

        await _appDbContext.SaveChangesAsync();

        return userInfos;
    }

    public async Task<string> UpdatePassword(Guid identity, string newPassword)
    {
        var user = await _appDbContext.Users.FindAsync(identity);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        await _appDbContext.Users
            .Where(x => x.Id == identity)
            .ExecuteUpdateAsync(
                u => u.SetProperty(p => p.Password, newPassword)
        );

        return "Senha alterada com sucesso";
    }

    public async Task DeleteUserAsync(Guid identity)
    {
        var user = await _appDbContext.Users.FindAsync(identity);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        _appDbContext.Users.Remove(user);

        await _appDbContext.SaveChangesAsync();
    }
}