namespace TicketSelling.Core.Entities;

public class OrderItem: BaseEntity
{
    public int OrderId { get; set; }
    public int MerchandiseId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public Merchandise? Merchandise { get; set; }
    public Order? Order { get; set; }
}
