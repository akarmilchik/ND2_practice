namespace TicketsResale.Business.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public OrderStatuses Status { get; set; }
        public string BuyerId { get; set; }
        public StoreUser Buyer { get; set; }
        public string TrackNumber { get; set; }
    }
}
