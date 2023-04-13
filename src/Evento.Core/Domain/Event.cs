using System.ComponentModel.DataAnnotations.Schema;

namespace Evento.Core.Domain
{
    [Table("Event")]
    public class Event
    {
        // Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedAt { get;  set; }

        // Nawigation Propierties
        public virtual ICollection<Ticket> Tickets { get; set; }


        public Event()
        {
            Name = string.Empty;
            Acronym = string.Empty;
            Description = string.Empty;
            Tickets = new List<Ticket>();
        }

        public Event(string name, string description, DateTime startDate, DateTime endDate)
        {
            SetName(name);
            SetDescription(description);
            SetDates(startDate, endDate);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void SetDates(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new Exception($"Event with id {ID} must have end date greater than start date.");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception($"Event with id: '{ID}' can not have an empty name!");

            Name = name;
            Acronym = name.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception($"Event with id: '{ID}' can not have an empty description!");

            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
