using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
        {
            var @event = await repository.GetAsync(id);

            if (@event == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist!");
            }

            return @event;
        }

        public static async Task<Ticket> GetOrFailAsync(this IEventRepository repository, Guid eventId, Guid ticketId)
        {
            var @event = await repository.GetOrFailAsync(eventId);
            var ticket = @event.PurchasedTickets.SingleOrDefault(x => x.Id == ticketId);
            if (ticket == null)
            {
                throw new Exception($"Ticket with id: '{ticketId}' was not found for event id '{@event.Name}'.");
            }

            return ticket;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
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
