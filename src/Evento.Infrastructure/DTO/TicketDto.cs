using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int Seating { get; set; }
        public decimal Price { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? PurchaseAt { get; set; }
        public bool Purchased { get; set; }
    }
}
