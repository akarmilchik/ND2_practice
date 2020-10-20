using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<TicketsCart> TicketsCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Venue>().ToTable("Venues");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            modelBuilder.Entity<TicketsCart>().ToTable("TicketsCarts");

            modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.TicketsCartId, ci.TicketId });
            modelBuilder.Entity<Event>().HasKey(ev => new { ev.VenueId });
            modelBuilder.Entity<Order>().HasKey(ord => new { ord.TicketId, ord.BuyerId });
            modelBuilder.Entity<Ticket>().HasKey(t => new { t.EventId, t.SellerId });
            modelBuilder.Entity<Venue>().HasKey(v => new { v.CityId });
        }
    }
    
}
