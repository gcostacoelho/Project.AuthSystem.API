namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface IAuthService
{
    Task<string> GenerateTokenAsync(string email, string password);
    Task<bool> ValidateToken(string token);    
}