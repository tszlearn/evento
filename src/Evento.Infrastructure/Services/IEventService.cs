using Evento.Core.Domain;
using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDetailsDto> GetEventAsync(int id);
        Task<EventDetailsDto> GetEventAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync(string name="");
        Task<EventDto> CreateAsync(string name, string description, DateTime startDate, DateTime endDate);
        Task AddTicketAsync(int eventId, int amount, decimal price);
        Task UpdateAsync(int id, string name, string description);
        Task DeleteAsync(int id);
    }
}
