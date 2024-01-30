using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Models;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Identity { get; set; }
    public string Fullname { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}