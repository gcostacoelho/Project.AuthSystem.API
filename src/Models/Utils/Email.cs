namespace Project.AuthSystem.API.src.Models.Utils;
public class Email(string fullname, string email, string subject, string body)
{
    public string Name { get; set; } = fullname;
    public string To { get; set; } = email;
    public string Subject { get; set; } = subject;
    public string Body { get; set; } = body;
}