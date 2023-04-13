using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class AccountController: ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;

        public AccountController(IUserService userService, ITicketService ticketService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
            => Json(await _userService.GetAccountAsync(UserId.Value));

        [HttpGet("tickets")]
        [Authorize]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetForUserAsync(UserId.Value);

            return Json(tickets);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser command)
        {
            if (command == null)
            {
                return NoContent();
            }

            await _userService.RegisterAsync(command.Username, command.Password, command.Email, command.Role);

            return Created($"/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser command)
        {
            try
            {
                return Json(await _userService.LoginAsync(command.Email, command.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> Forgot([FromBody] ForgotUser command)
        {
            try
            {
                await _userService.ForgotPassword(command.Email);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset_password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword command)
        {
            try
            {
                await _userService.ResetPassword(command.Token, command.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
