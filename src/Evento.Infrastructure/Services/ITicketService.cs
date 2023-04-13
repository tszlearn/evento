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
        Task<TicketDto> GetAsync(int userId, int eventId, int ticketId);
        Task<IEnumerable<TicketDetailsDto>> GetForUserAsync(int userId);
        Task PurchaseAsync(int eventId, int userId, int amount);
        Task CancelPurchesedAsync(int eventId, int userId, int amount)
;
    }
}
