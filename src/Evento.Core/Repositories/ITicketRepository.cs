using Evento.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Core.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicketsAsync(IEnumerable<Ticket> tickets);
        Task<Ticket?> GetAsync(int id);
        Task<IEnumerable<Ticket>> GetFreeTicketAsync(Event @event, int? amount);
        Task<IEnumerable<Ticket>> GetUsersTicketForEvent(Event @event, User user, int? amount);
        Task UpdateAsync(IEnumerable<Ticket> tickets);
    }
}
