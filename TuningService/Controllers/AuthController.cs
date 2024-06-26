using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tuning.Library.Auth;
using TuningService.DTOs;

namespace TuningService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var result = await _mediator.Send(new RegisterUser.Command(registrationDto));
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _mediator.Send(new ConfirmEmail.Command(new ConfirmEmailDto { UserId = userId, Token = token }));
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginDto)
        {
            var result = await _mediator.Send(new LoginUser.Command(loginDto));
            if (result.Success)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }
    }

}
