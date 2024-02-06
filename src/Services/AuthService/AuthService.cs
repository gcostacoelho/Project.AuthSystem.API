using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.AuthService;
public class AuthService(IAppSettings appSettings) : IAuthService
{
    private readonly IAppSettings _appSettings = appSettings;

    public string GenerateTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}