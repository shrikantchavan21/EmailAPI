using EmailAPI.Models;
using EmailAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APITestEmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        public APITestEmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                await emailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}