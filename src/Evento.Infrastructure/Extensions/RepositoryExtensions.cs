using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, int id)
        {
            var @event = await repository.GetAsync(id);

            if (@event == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist!");
            }

            return @event;
        }

        public static async Task<Ticket> GetOrFailAsync(this ITicketRepository repository, int id)
        {
            var ticket = await repository.GetAsync(id);

            if (ticket == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist!");
            }

            return ticket;
        }

        public static async Task<Ticket> GetOrFailAsync(this IEventRepository repository, int eventId, int ticketId)
        {
            var @event = await repository.GetOrFailAsync(eventId);
            var ticket = @event.Tickets.SingleOrDefault(x => x.ID == ticketId);
            if (ticket == null)
            {
                throw new Exception($"Ticket with id: '{ticketId}' was not found for event id '{@event.Name}'.");
            }

            return ticket;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, int id)
        {
            var user = await repository.GetAsync(id);

            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist!");
            }

            return user;
        }
    }
}
