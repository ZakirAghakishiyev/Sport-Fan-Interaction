namespace TicketSelling.Core.Entities
{
    public class Order: BaseEntity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; } = [];
        public Payment? Payment { get; set; }
        public AppUser? User { get; set; }
    }
}
