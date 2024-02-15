using System.Net;
using MailKit.Net.Smtp;
using MimeKit;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Services.SmtpService;
public class SmtpService(IAppSettings appSettings) : ISmtpService
{
    private readonly IAppSettings _appSettings = appSettings;

    public void SendEmail(Email email)
    {
        try
        {
            var mimeMessage = new MimeMessage();
            var smtp = new SmtpClient();

            mimeMessage.From.Add(new MailboxAddress(_appSettings.SmtpUsername, _appSettings.SmtpEmail));
            mimeMessage.To.Add(new MailboxAddress(email.Name, email.To));

            mimeMessage.Subject = email.Subject;

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = email.Body
            };

            smtp.Connect("smtp.gmail.com", 587, false);

            smtp.Authenticate(_appSettings.SmtpUsername, _appSettings.SmtpPassword);

            smtp.Send(mimeMessage);

            smtp.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ApiException("Erro ao enviar email, tente novamente mais tarde", HttpStatusCode.InternalServerError);
        }
    }
}