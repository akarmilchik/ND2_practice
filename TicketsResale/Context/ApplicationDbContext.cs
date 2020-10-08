using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TicketsResale.Business.Models;
using TicketsResale.Context;

namespace TicketsResale.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
