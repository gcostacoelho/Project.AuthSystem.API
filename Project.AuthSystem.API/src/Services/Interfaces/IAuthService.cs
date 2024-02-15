using Project.AuthSystem.API.src.Models.Users;

namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface IAuthService
{
    string GenerateTokenAsync(User user);   
}