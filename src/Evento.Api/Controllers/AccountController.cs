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
            => Json(await _userService.GetAccountAsync(UserId));

        [HttpGet("tickets")]
        [Authorize]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetForUserAsync(UserId);

            return Json(tickets);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser command)
        {
            if (command == null)
            {
                return NoContent();
            }

            command.UserId = Guid.NewGuid();
            await _userService.RegisterAsync(command.UserId, command.Username, command.Password, command.Email, command.Role);

            return Created($"/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));
    }
}
