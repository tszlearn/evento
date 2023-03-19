﻿namespace Evento.Core.Domain
{
    public class Ticket : Entity
    {
        public Guid EventId { get; protected set; }
        public int Seating { get; protected set; }
        public decimal Price { get; protected set; }
        public Guid? UserId { get; protected set; }
        public string Username { get; protected set; }
        public DateTime? PurchaseAt { get; protected set; }
        public bool Purchased => UserId.HasValue;

        protected Ticket() { }

        public Ticket(Event @event, int seating, decimal price)
        {
            EventId = @event.Id;
            Seating = seating;
            Price = price;
        }

        public void Purchase(User user)
        {
            if (Purchased)
            {
                throw new Exception($"Ticket was already purchesed by user: {Username} at {PurchaseAt}.");
            }

            UserId = user.Id;
            Username= user.Name;
            PurchaseAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (!Purchased)
            {
                throw new Exception("Ticket was not purchesed and can not be canceled.");
            }

            UserId = null;
            Username = null;
            PurchaseAt = null;
        }
    }
}
