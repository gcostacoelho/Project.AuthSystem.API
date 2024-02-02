using Project.AuthSystem.API.src.Interfaces;

namespace Project.AuthSystem.API.src.Models.Utils;
public class AppSettings : IAppSettings
{
    private readonly IConfiguration _config;

    public AppSettings(IConfiguration configuration)
    {
        _config = configuration;
    }

    public string HashSalt => _config["AppSettings:HashSalt"];
}