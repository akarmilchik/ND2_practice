namespace TicketsResale.Controllers.Api.Models
{
    public class OrderResource
    {
        public int Id { get; set; }
        public OrderItemResource[] Items { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderItemResource
    {
        public ListingResource Listing { get; set; }
        public int Amount { get; set; }
    }
}
