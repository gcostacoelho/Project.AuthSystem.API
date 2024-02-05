using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;

using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.AuthService;
public class AuthService(IAppSettings appSettings) : IAuthService
{
    private readonly IAppSettings _appSettings = appSettings;

    public string GenerateTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Fullname.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}