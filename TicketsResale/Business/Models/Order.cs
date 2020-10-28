using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket{ get; set; }

        public int TicketsCartId { get; set; }

        public TicketsCart TicketsCart { get; set; }

        public int BuyerId { get; set; }

        public StoreUser Buyer { get; set; }

        public byte Status { get; set; }

        public string TrackNumber { get; set; }

    }
}
