using Project.AuthSystem.API.src.Interfaces;

namespace Project.AuthSystem.API.src.Models.Utils;
public class AppSettings(IConfiguration configuration) : IAppSettings
{
    private readonly IConfiguration _config = configuration;

    public string HashSalt => _config["AppSettings:HashSalt"];
    public string Secret => _config["AppSettings:Secret"];

    public string SmtpEmail => _config["AppSettings:SmtpEmail"];
    public string SmtpUsername => _config["AppSettings:SmtpUsername"];
    public string SmtpPassword => _config["AppSettings:SmtpPassword"];
}