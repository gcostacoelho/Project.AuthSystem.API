using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Facades.Interfaces.Login;

namespace Project.AuthSystem.API.src.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ILoginFacade loginFacade) : ControllerBase
    {
        private readonly ILoginFacade _loginFacade = loginFacade;

        [HttpGet("generate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTokenAsync([FromHeader, Required] string email, [FromHeader, Required] string password)
        {
            var response = await _loginFacade.GetTokenAsync(email, password);

            return Ok(response);
        }

        /* 
        [HttpGet("validate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateTokenAsync([FromHeader, Required] string authorization)
        {
            var response = await _loginFacade.ValidateToken(authorization);

            return Ok(response);
        }
        */
    }
}