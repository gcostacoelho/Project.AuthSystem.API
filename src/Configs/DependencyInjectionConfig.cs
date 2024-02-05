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

        #endregion


        #region Facades

        services.AddScoped<ILoginFacade, LoginFacade>();

        #endregion

        services.AddSingleton<IAppSettings, AppSettings>();
    }

    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration.GetSection("AppSettings:Secret");
        var keyEncoded = Encoding.ASCII.GetBytes(key.ToString());

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyEncoded),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}