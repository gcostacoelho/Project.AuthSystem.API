using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project.AuthSystem.API.src.Facades.Interfaces.UserFacadeInterface;
using Project.AuthSystem.API.src.Models.Users;
using Project.AuthSystem.API.src.Models.Utils;

namespace Project.AuthSystem.API.src.Controllers.UserController;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IUserFacade userFacade) : ControllerBase
{
    private readonly IUserFacade _userFacade = userFacade;

    [HttpGet]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserAsync([FromHeader, Required] string email)
    {
        var response = await _userFacade.GetUserAsync(email);

        return Ok(response);
    }

    [HttpPost, AllowAnonymous]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostNewUserAsync([FromBody, Required] UserDto user)
    {
        var response = await _userFacade.NewUserAsync(user);

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUserAsync([FromBody, Required] UserDtoWithoutPass user, [FromHeader, Required] string email)
    {
        var response = await _userFacade.UpdateUserAsync(user, email);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUPasswordserAsync([FromHeader, Required] string email, [FromHeader, Required] string newPass, [FromHeader, Required] string oldPass)
    {
        var response = await _userFacade.UpdatePassword(email, newPass, oldPass);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync([FromHeader, Required] string email)
    {
        await _userFacade.DeleteUserAsync(email);

        return Ok();
    }
}