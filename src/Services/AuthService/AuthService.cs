using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Project.AuthSystem.API.src.Services.AuthService;
public class AuthService(IAppSettings appSettings) : IAuthService
{
    private readonly IAppSettings _appSettings = appSettings;

    public string GenerateTokenAsync(User user)
    {
        var secretEncoded = Encoding.UTF8.GetBytes(_appSettings.Secret);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(secretEncoded);

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken (
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}