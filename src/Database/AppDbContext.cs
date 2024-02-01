using Microsoft.EntityFrameworkCore;
using Project.AuthSystem.API.src.Models.Users;

namespace Project.AuthSystem.API.src.Database;

// * Using a primary constructor, new feature from CSharp
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}