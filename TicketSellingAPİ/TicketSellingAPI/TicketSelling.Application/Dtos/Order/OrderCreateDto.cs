using TicketSelling.Application.Dtos.OrderItem;

namespace TicketSelling.Application.Dtos.Order;

public class OrderCreateDto
{
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public List<OrderItemCreateDto> Items { get; set; } = [];
}

