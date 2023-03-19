using Evento.Core.Domain;
using Evento.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public interface ITicketService
    {
        Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId);
        Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(Guid userId);
        Task PurchaseAsync(Guid eventId, Guid userId, int amount);
        Task CancelPurchesedAsync(Guid eventId, Guid userId, int amount)
;
    }
}
