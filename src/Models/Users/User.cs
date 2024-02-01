using Microsoft.EntityFrameworkCore;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Models.Users;

// * Using a primary constructor , new feature from CSharp
[Index(nameof(Email), IsUnique = true)]
public class User(string email, string password, string fullname, DateTime birthday) : BaseModel
{

    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string Fullname { get; set; } = fullname;
    public DateTime Birthday { get; set; } = birthday;

    public User(string email, string password, string fullname, DateTime birthday, Guid identity) : this(email, password, fullname, birthday)
    {
        Id = identity;
    }
}