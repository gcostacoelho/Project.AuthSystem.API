namespace Project.AuthSystem.API.src.Models.Utils;
public abstract class BaseModel
{
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set; } = DateTime.UtcNow;
}