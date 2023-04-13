using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [Route("events/{eventId}/tickets")]
    [Authorize]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        //Get
        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicket(int eventId, int ticketId)
        {
            var ticket = await _ticketService.GetAsync(UserId.Value, eventId, ticketId);

            if(ticket == null)
            {
                return NotFound();
            }

            return Json(ticket);
        }

        //Post
        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> PurchesTickets(int eventId, int amount)
        {
            await _ticketService.PurchaseAsync(eventId, UserId.Value, amount);

            return NoContent();
        }

        //Delete
        [HttpDelete("cancel/{amount}")]
        public async Task<IActionResult> CancelPurchesedTickets(int eventId, int amount)
        {
            await _ticketService.CancelPurchesedAsync(eventId, UserId.Value, amount);

            return NoContent();
        }
    }
}
