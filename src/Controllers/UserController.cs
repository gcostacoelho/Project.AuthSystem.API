using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

}