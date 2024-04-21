using CryptoAvenue.Application.UserApp.UserCommands;
using CryptoAvenue.Domain.Models;
using CryptoAvenue.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAvenue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var command = new LoginUser { Model = loginModel };
            var response = await _mediator.Send(command);

            if(response != null)
            {
                return Ok(response);
            }
            return Unauthorized("Invalid credentials.");
        }
    }
}
