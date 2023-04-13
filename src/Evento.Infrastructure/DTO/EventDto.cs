using Evento.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.DTO
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TicketCount { get; set; }
        public int PurchasedTicketsCount { get; set; }
        public int AvailableTicketsCount { get; set; }
    }
}
