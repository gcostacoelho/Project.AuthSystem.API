using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Configs;
public static class DependencyInjectionConfig
{
    public static void RegisterDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("DockerConnection")));
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppSettings, AppSettings>();
    }
}