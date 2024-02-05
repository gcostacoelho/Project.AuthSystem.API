using System.Net;

using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.UserService;
public class UserService(AppDbContext appDbContext, IHashService hashService) : IUserService
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly IHashService _hashService = hashService;

    public async Task<User> GetUserAsync(string email)
    {
        var user = await _appDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        return user;
    }

    public async Task<User> NewUserAsync(UserDto user)
    {
        user.Password = _hashService.EncryptyText(user.Password);

        var userComp = new User(user.Email, user.Password, user.Fullname, user.Birthday);

        _appDbContext.Users.Add(userComp);

        await _appDbContext.SaveChangesAsync();

        return userComp;
    }

    public async Task<User> UpdateUserAsync(UserDtoWithoutPass user, string email)
    {
        var userInfos = await _appDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (userInfos == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        // ! Search a better way to do this without generate error in sql server;

        userInfos.Email = user.Email;
        userInfos.Fullname = user.Fullname;
        userInfos.Birthday = user.Birthday;

        _appDbContext.Entry(userInfos).State = EntityState.Modified;

        await _appDbContext.SaveChangesAsync();

        return userInfos;
    }

    public async Task<string> UpdatePassword(string email, string newPassword, string oldPassword)
    {
        var user = await _appDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        var isSameHash = _hashService.CompareHashes(user.Password, oldPassword);

        if (!isSameHash)
        {
            throw new ApiException(Constants.NOT_POSSIBLE_CHANGE, HttpStatusCode.BadRequest);
        }

        var passwordEncripted = _hashService.EncryptyText(newPassword);

        await _appDbContext.Users
            .Where(x => x.Email == email)
            .ExecuteUpdateAsync(
                u => u.SetProperty(p => p.Password, passwordEncripted)
        );

        return "Senha alterada com sucesso";
    }

    public async Task DeleteUserAsync(string email)
    {
        var user = await _appDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.NotFound);
        }

        _appDbContext.Users.Remove(user);

        await _appDbContext.SaveChangesAsync();
    }
}