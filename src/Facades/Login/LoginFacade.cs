using System.Net;
using Project.AuthSystem.API.src.Facades.Interfaces.Login;
using Project.AuthSystem.API.src.Models;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Facades.Login;
public class LoginFacade(IAuthService authService, IUserService userService, IHashService hashService) : ILoginFacade
{
    private readonly IAuthService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IHashService _hashSevice = hashService;

    public async Task<string> GetTokenAsync(string email, string password)
    {
        var user = await _userService.GetUserAsync(email);

        var isPasswordEqualWithHash = _hashSevice.CompareHashes(user.Password, password);

        if (!isPasswordEqualWithHash)
        {
            throw new ApiException(Constants.UNAUTHORIZED, HttpStatusCode.Unauthorized);
        }

        var token = await _authService.GenerateTokenAsync(user);

        return token;
    }
}