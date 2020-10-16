﻿namespace TicketsResale.Business.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Ticket Ticket { get; set; }

        public OrderStatuses Status { get; set; }

        public User Buyer { get; set; }

        public string TrackNumber { get; set; }

    }
}