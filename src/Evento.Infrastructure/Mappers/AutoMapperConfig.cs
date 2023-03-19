using AutoMapper;
using Evento.Core.Domain;
using Evento.Infrastructure.DTO;

namespace Evento.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initailize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x=>x.TicketCount, m=>m.MapFrom(p=>p.Tickets.Count()));
                cfg.CreateMap<Event, EventDetailsDto>()
                    .ForMember(x=>x.PurchasedTicketsCount, m=>m.MapFrom(p=>p.PurchasedTickets.Count()))
                    .ForMember(x=>x.AvailableTicketsCount, m=>m.MapFrom(p=>p.AvailableTickets.Count()));
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<Ticket, TicketDetailsDto>();
                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
    }
}
