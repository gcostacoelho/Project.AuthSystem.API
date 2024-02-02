using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Controllers.UserController;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserAsync([FromHeader, Required] Guid identity)
    {
        var response = await _userService.GetUserAsync(identity);

        return Ok(response);
    }

    [HttpPost]
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
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserAsync([FromBody, Required] UserDtoWithoutPass user, [FromHeader, Required] Guid identity)
    {
        var response = await _userService.UpdateUserAsync(user, identity);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUPasswordserAsync([FromHeader, Required] Guid identity, [FromHeader, Required] string newPass, [FromHeader, Required] string oldPass)
    {
        var response = await _userService.UpdatePassword(identity, newPass, oldPass);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync([FromHeader, Required] Guid identity)
    {
        await _userService.DeleteUserAsync(identity);

        return Ok();
    }
}