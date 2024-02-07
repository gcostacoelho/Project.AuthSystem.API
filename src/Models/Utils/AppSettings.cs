using Project.AuthSystem.API.src.Interfaces;

namespace Project.AuthSystem.API.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

    public string HashSalt => _config["AppSettings:HashSalt"];
    public string Secret => _config["AppSettings:Secret"];
}