using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketsResale.Business.Models;

namespace TicketsResale.Context
{
    public class StoreContext : IdentityDbContext<StoreUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {  }

        public DbSet<City> Cities { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<EventCategory>().ToTable("EventCategories");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Venue>().ToTable("Venues");
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }
    
}
