namespace Project.AuthSystem.API.src.Facades.Interfaces.Login;
public interface ILoginFacade
{
    Task<string> GetTokenAsync(string email, string password);
}