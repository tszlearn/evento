using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventoContext _context;

        public EventRepository(EventoContext context)
        {
            _context = context;
        }

        public async Task<Event> GetAsync(int id)
            => await _context.Events.FirstOrDefaultAsync(x => x.ID == id);

        public async Task<Event> GetAsync(string name)
            => await _context.Events.FirstOrDefaultAsync(x => x.Acronym.Equals(name.ToLowerInvariant()));

        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            if (!string.IsNullOrEmpty(name))
            {
                return await _context.Events.Where(x => x.Name.ToLowerInvariant()
                        .Contains(name.ToLowerInvariant())).ToListAsync();
            }
            else
            {
                return await _context.Events.ToListAsync();
            }
        }

        public async Task<Event> AddAsync(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
            return @event;
        }

        public async Task DeleteAsync(Event @event)
        {
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event @event)
        {
            _context.Events.Update(@event);
            await _context.SaveChangesAsync();
        }
    }
}
