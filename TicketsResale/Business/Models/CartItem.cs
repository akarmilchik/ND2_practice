namespace TicketsResale.Business.Models
{
    public class CartItem
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketsCartId { get; set; }
        public TicketsCart TicketsCart { get; set; }
        public int Count { get; set; }
        public byte Status { get; set; }
        //public int BuyerId { get; set; }
        public StoreUser Buyer { get; set; }
        public string TrackNumber { get; set; }
    }
}
