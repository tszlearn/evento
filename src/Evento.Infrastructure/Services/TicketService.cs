using AutoMapper;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Repositories;

namespace Evento.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TicketService(IEventRepository eventRepository, IUserRepository userRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _mapper=mapper;
        }

        public async Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId)
        {
            var ticket = await _eventRepository.GetOrFailAsync(eventId, ticketId);
            var user = await _userRepository.GetOrFailAsync(userId);

            if(ticket.UserId != user.Id)
            {
                throw new Exception($"User '{user.Name}' did not buy ticket with id '{ticketId}'");
            }

            return _mapper.Map<TicketDto>(ticket);  
        }

        public async Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var events = await _eventRepository.BroseAsync();
            var allTickets = new List<TicketDetailsDto>();

            foreach(var @event in events) 
            {
                var tickets = _mapper.Map<IEnumerable<TicketDetailsDto>>(@event.GetUserTickets(user)).ToList();
                tickets.ForEach(x => { x.EventId = @event.Id; x.EventName = @event.Name; });
                allTickets.AddRange(tickets);
            }

            return allTickets;    
        }

        public async Task CancelPurchesedAsync(Guid eventId, Guid userId, int amount)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var user = await _userRepository.GetOrFailAsync(userId);

            @event.CancelPutchesedTickets(user, amount);

            await _eventRepository.UpdateAsync(@event);
        }

        

        public async Task PurchaseAsync(Guid eventId, Guid userId, int amount)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var user = await _userRepository.GetOrFailAsync(userId);

            @event.PurcheseTicket(user, amount);

            await _eventRepository.UpdateAsync(@event);
        }
    }
}
