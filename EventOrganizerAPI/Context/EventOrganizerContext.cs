using EventOrganizerAPI.Context;
using EventOrganizerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizerAPI.Context
{
    public class EventOrganizerContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
