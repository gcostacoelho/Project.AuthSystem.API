using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Services.Interfaces;
public interface ISmtpService
{
    void SendEmail(Email email);
}