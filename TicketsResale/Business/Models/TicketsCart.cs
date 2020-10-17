using System.Collections.Generic;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class TicketsCart : IEntity
    {
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

    }
}
