using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, ITicketRepository ticketRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailsDto> GetEventAsync(int id)
        {
            var @event = await _eventRepository.GetAsync(id);

            return _mapper.Map<EventDetailsDto>(@event);
        }

        public async Task<EventDetailsDto> GetEventAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);

            

            return _mapper.Map<EventDetailsDto>(@event);
        }

        public async Task<IEnumerable<EventDto>> BrowseAsync(string name = "")
        {
            var events = await _eventRepository.BrowseAsync(name);

            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task AddTicketAsync(int eventId, int amount, decimal price)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);
            var tickets = new HashSet<Ticket>();

            for (int i = 0; i < amount; i++)
            {
                tickets.Add(new Ticket(@event, tickets.Count + 1, price));
            }

            await _ticketRepository.AddTicketsAsync(tickets);
        }



        public async Task<EventDto> CreateAsync(string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(name);

            if (@event != null)
            {
                throw new Exception($"Event name: '{name}' already exists!");
            }

            @event = new Event(name, description, startDate, endDate);

            var result = await _eventRepository.AddAsync(@event);

            return _mapper.Map<EventDto>(result);
        }

        public async Task DeleteAsync(int id)
        {
            var @event = await _eventRepository.GetOrFailAsync(id);
            await _eventRepository.DeleteAsync(@event);
        }

        public async Task UpdateAsync(int id, string name, string description)
        {
            var @event = await _eventRepository.GetAsync(name);

            if (@event != null)
            {
                throw new Exception($"Event name: '{name}' already exists!");
            }

            @event = await _eventRepository.GetOrFailAsync(id);

            @event.SetName(name);
            @event.SetDescription(description);
            await _eventRepository.UpdateAsync(@event);
        }
    }
}
