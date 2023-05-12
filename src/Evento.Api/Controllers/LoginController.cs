using Evento.Infrastructure.Commands.Authentication;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate", Name = "Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                return Ok(await _authenticationService.AuthenticateAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (request == null)
            {
                return NoContent();
            }

            return Ok(await _authenticationService.RegisterAsync(request));
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> Forgot([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                return Ok(await _authenticationService.SignForgotPasswordAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset_password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                return Ok(await _authenticationService.ResetPasswordAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
