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
    }
}
