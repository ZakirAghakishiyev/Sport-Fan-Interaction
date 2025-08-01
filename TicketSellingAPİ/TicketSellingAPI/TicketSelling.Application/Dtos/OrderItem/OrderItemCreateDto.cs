namespace TicketSelling.Application.Dtos.OrderItem;

public class OrderItemCreateDto
{
    public int MerchandiseId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
}
