﻿namespace TicketsResale.Business.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public User Seller { get; set; }

        public TicketStatuses Status { get; set; }
    }
}