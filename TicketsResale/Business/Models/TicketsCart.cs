using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketsResale.Context;

namespace TicketsResale.Business.Models
{
    public class TicketsCart : IEntity
    {
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        //public StoreUser StoreUser { get; set; }

    }
}
