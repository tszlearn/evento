using AutoMapper;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(IEventRepository eventRepository, IUserRepository userRepository, ITicketRepository ticketRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _mapper=mapper;
        }

        public async Task<TicketDto> GetAsync(int userId, int eventId, int ticketId)
        {
            var ticket = await _eventRepository.GetOrFailAsync(eventId, ticketId);
            var user = await _userRepository.GetOrFailAsync(userId);

            if(ticket.UserID != user.ID)
            {
                throw new Exception($"User '{user.Name}' did not buy ticket with id '{ticketId}'");
            }

            return _mapper.Map<TicketDto>(ticket);  
        }

        public async Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(int userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var allTickets = _mapper.Map<IEnumerable<TicketDetailsDto>>(user.Ticket).ToList();

            return allTickets;    
        }

        public async Task CancelPurchesedAsync(int eventId, int userId, int amount)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var user = await _userRepository.GetOrFailAsync(userId);
            var purchesdTickets = await _ticketRepository.GetUsersTicketForEvent(@event, user, amount);
  

            if (purchesdTickets.Count() < amount)
            {
                throw new Exception($"Not enougth purchesed tickets to be canceled ({amount}) by user '{user.Name}'.");
            }

            foreach (var ticket in purchesdTickets)
            {
                ticket.UserID = null;
                ticket.PurchaseAt = null;
            }

            await _ticketRepository.UpdateAsync(purchesdTickets);
        }

        

        public async Task PurchaseAsync(int eventId, int userId, int amount)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var user = await _userRepository.GetOrFailAsync(userId);

            var avaiableTickets = await _ticketRepository.GetFreeTicketAsync(@event, amount);

            if (avaiableTickets.Count() < amount)
            {
                throw new Exception($"Not enough availible tickets to purchase ({amount}) by user '{user.Name}'");
            }

            foreach (var ticket in avaiableTickets)
            {
                ticket.UserID = user.ID;
                ticket.PurchaseAt = DateTime.UtcNow;
            }

            await _ticketRepository.UpdateAsync(avaiableTickets);
        }
    }
}
