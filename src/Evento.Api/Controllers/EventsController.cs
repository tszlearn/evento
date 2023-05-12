using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class EventsController: ApiControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name="")
        {
            IEnumerable<EventDto> events = await _eventService.BrowseAsync(name);

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            var @event = await _eventService.GetEventAsync(eventId);

            if(@event == null)
            {
                return NotFound();
            }

            return Json(@event);
        }

        [HttpPost]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            if(command == null)
            {
                return NoContent();
            }

            var @event = await _eventService.CreateAsync(command.Name, 
                command.Description, command.StartDate, command.EndDate);

            await _eventService.AddTicketAsync(@event.Id, command.Tickets, command.Price);

            return Created($"/events/{@event.Id}", null);
        }

        [HttpPut("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Put(int eventId, [FromBody] UpdateEvent command)
        {
            command.EventId = Guid.NewGuid();
            await _eventService.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Delete(int eventId)
        {
            await _eventService.DeleteAsync(eventId);

            return NoContent();
        }
    }
}
