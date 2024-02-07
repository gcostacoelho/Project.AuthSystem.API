using Microsoft.EntityFrameworkCore;

using Project.AuthSystem.API.src.Database;
using Project.AuthSystem.API.src.Interfaces;
using Project.AuthSystem.API.src.Models.Utils;

using Project.AuthSystem.API.src.Services.HashService;
using Project.AuthSystem.API.src.Services.Interfaces;
using Project.AuthSystem.API.src.Services.AuthService;
using Project.AuthSystem.API.src.Services.UserService;
using Project.AuthSystem.API.src.Facades.Interfaces.Login;
using Project.AuthSystem.API.src.Facades.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project.AuthSystem.API.src.Services.SmtpService;

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
        #region Services

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISmtpService, SmtpService>();

        #endregion


        #region Facades

        services.AddScoped<ILoginFacade, LoginFacade>();

        #endregion

        services.AddSingleton<IAppSettings, AppSettings>();
    }

    public static void RegisterAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Secret").Value);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(op =>
        {
            op.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = false
            };
        });

    }
}