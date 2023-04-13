using Evento.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Evento.Infrastructure.Context
{
    public class EventoContext:DbContext
    {
        public EventoContext(DbContextOptions<EventoContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
        }
    }
}
