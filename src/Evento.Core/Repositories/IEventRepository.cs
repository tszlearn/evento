using Evento.Core.Domain;

namespace Evento.Core.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetAsync(int id);
        Task<Event> GetAsync(string name);
        Task<IEnumerable<Event>> BrowseAsync(string name="");
        Task<Event> AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(Event @event);
    }
}
