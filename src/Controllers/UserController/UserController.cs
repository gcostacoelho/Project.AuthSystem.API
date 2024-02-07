using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Controllers.UserController;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserAsync([FromHeader, Required] string email)
    {
        var response = await _userService.GetUserAsync(email);

        return Ok(response);
    }

    [HttpPost, AllowAnonymous]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostNewUserAsync([FromBody, Required] UserDto user)
    {
        var response = await _userService.NewUserAsync(user);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserAsync([FromBody, Required] UserDtoWithoutPass user, [FromHeader, Required] string email)
    {
        var response = await _userService.UpdateUserAsync(user, email);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUPasswordserAsync([FromHeader, Required] string email, [FromHeader, Required] string newPass, [FromHeader, Required] string oldPass)
    {
        var response = await _userService.UpdatePassword(email, newPass, oldPass);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync([FromHeader, Required] string email)
    {
        await _userService.DeleteUserAsync(email);

        return Ok();
    }
}