namespace Project.AuthSystem.API.src.Models.Users;
public class UserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public DateTime Birthday { get; set; }
}