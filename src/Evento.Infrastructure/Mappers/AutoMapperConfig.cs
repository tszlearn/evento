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
                    .ForMember(x => x.TicketCount, m => m.MapFrom(p => p.Tickets.Count()))
                    .ForMember(x => x.PurchasedTicketsCount, m => m.MapFrom(p => p.Tickets.Where(x => x.UserID.HasValue).ToList().Count()))
                    .ForMember(x => x.AvailableTicketsCount, m => m.MapFrom(p => p.Tickets.Where(x => !x.UserID.HasValue).ToList().Count()));

                cfg.CreateMap<Event, EventDetailsDto>()
                    .ForMember(x => x.TicketCount, m => m.MapFrom(p => p.Tickets.Count()))
                    .ForMember(x => x.PurchasedTicketsCount, m => m.MapFrom(p => p.Tickets.Where(x => x.UserID.HasValue).ToList().Count()))
                    .ForMember(x => x.AvailableTicketsCount, m => m.MapFrom(p => p.Tickets.Where(x => !x.UserID.HasValue).ToList().Count()));

                cfg.CreateMap<Ticket, TicketDto>()
                    .ForMember(x => x.UserName, m => m.MapFrom(p => p.User.Name))
                    .ForMember(x=>x.Purchased, m=>m.MapFrom(p=>p.UserID.HasValue));

                cfg.CreateMap<Ticket, TicketDetailsDto>()
                    .ForMember(x => x.UserName, m => m.MapFrom(p => p.User.Name))
                    .ForMember(x => x.Purchased, m => m.MapFrom(p => p.UserID.HasValue))
                    .ForMember(x => x.EventName, m => m.MapFrom(p => p.Event.Name));

                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
    }
}
