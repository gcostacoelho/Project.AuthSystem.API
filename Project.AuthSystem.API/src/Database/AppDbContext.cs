using Microsoft.EntityFrameworkCore;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Database;

// * Using a primary constructor, new feature from CSharp
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }


    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        AddTimestamps();
        return await base.SaveChangesAsync();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow; // current datetime

            if (entity.State == EntityState.Added)
            {
                ((BaseModel)entity.Entity).CreatedAt = now;
            }
            ((BaseModel)entity.Entity).UpdatedAt = now;
        }
    }
}