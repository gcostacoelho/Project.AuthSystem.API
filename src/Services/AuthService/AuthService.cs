using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.AuthService;
public class AuthService(IHashService hashService) : IAuthService
{
    private readonly IHashService _hashService = hashService;

    public Task<string> GenerateTokenAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}