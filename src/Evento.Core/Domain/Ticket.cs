using System.ComponentModel.DataAnnotations.Schema;

namespace Evento.Core.Domain
{
    [Table("Ticket")]
    public class Ticket
    {
        // Properties
        public int ID { get; set; }
        public int EventID { get; set; }
        public int Seating { get; set; }
        public decimal Price { get; set; }
        public int? UserID { get; set; }
        public DateTime? PurchaseAt { get; set; }


        // Navigation Properties
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }



        public Ticket() { }

        public Ticket(Event @event, int seating, decimal price)
        {
            EventID = @event.ID;
            Seating = seating;
            Price = price;
        }
    }
}
