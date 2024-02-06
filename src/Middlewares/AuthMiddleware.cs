using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Middlewares;
public class AuthMiddleware(IAuthService authService, RequestDelegate next)
{
    private readonly IAuthService _authService = authService;
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context) { }
}