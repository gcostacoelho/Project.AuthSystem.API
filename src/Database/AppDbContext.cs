using Microsoft.EntityFrameworkCore;

namespace Project.AuthSystem.API.src.Database;

// * Using a primary constructor, new feature from CSharp
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) { }