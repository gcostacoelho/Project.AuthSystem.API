using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AuthSystem.API.src.Models.Utils;
using Project.AuthSystem.API.src.Services.Interfaces;

namespace Project.AuthSystem.API.src.Controllers.SendEmailController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SendEmailController(ISmtpService smtpService) : ControllerBase
    {
        private readonly ISmtpService _smtpService = smtpService;

        [HttpPost]
        public string SendEmail([FromBody, Required] Email email)
        {
            _smtpService.SendEmail(email);

            return "Email enviado com sucesso";    
        }
    }
}