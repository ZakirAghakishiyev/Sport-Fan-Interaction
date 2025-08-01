using TicketSelling.Application.Dtos.Merchandise;

namespace TicketSelling.Application.Dtos.OrderItem;

public class OrderItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }

    public MerchandiseDto? Merchandise { get; set; }
}

