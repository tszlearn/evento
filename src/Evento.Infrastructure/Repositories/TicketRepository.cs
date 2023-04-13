using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Evento.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EventoContext _context;

        public TicketRepository(EventoContext context) { _context = context; }

        public async Task AddTicketsAsync(IEnumerable<Ticket> tickets)
        {
            _context.AddRange(tickets);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetAsync(int id)
            => await _context.Tickets.FirstOrDefaultAsync(x => x.ID == id);

        public async Task<IEnumerable<Ticket>> GetFreeTicketAsync(Event @event, int? amount)
        {
            var freeTickets = await _context.Tickets.Where(x => x.EventID == @event.ID && x.UserID == null).ToListAsync();

            if (amount.HasValue)
            {
                return freeTickets.Take(amount.Value);
            }
            else
            {
                return freeTickets;
            }
        }

        public async Task<IEnumerable<Ticket>> GetUsersTicketForEvent(Event @event, User user, int? amount)
        {
            var tickets = await _context.Tickets.Where(x => x.EventID == @event.ID && x.UserID == user.ID ).ToListAsync();

            if (amount.HasValue)
            {
                return tickets.Take(amount.Value);
            }
            else
            {
                return tickets;
            }
        }

        public async Task UpdateAsync(IEnumerable<Ticket> tickets)
        {
            _context.Tickets.UpdateRange(tickets);
            await _context.SaveChangesAsync();
        }
    }
}
