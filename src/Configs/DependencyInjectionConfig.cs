using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.HashService;
using Project.AuthSystem.API.src.Services.Interfaces;
using Project.AuthSystem.API.src.Services.UserService;

namespace Project.AuthSystem.API.src.Configs;
public static class DependencyInjectionConfig
{
    public static void RegisterDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            op => op.UseSqlServer(
                configuration.GetConnectionString("DockerConnection")
            )
        );
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashService, HashService>();

        services.AddSingleton<IAppSettings, AppSettings>();
    }
}