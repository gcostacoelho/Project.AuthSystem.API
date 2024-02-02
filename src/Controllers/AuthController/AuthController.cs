using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpGet("generate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTokenAsync([FromHeader, Required] string email, [FromHeader, Required] string password)
        {
            var response = await _authService.GenerateTokenAsync(email, password);

            return Ok(response);
        }

        [HttpGet("validate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateTokenAsync([FromHeader, Required] string authorization)
        {
            var response = await _authService.ValidateToken(authorization);

            return Ok(response);
        }
    }
}