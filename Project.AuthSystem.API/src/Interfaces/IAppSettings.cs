namespace Project.AuthSystem.API.src.Interfaces;
public interface IAppSettings
{
    public string HashSalt { get; }
    public string Secret { get; }
    
    public string SmtpEmail { get; }
    public string SmtpUsername { get; }
    public string SmtpPassword { get; }
}