using TicketSelling.Application.Dtos.OrderItem;
using TicketSelling.Application.Dtos.Payment;
using TicketSelling.Application.Dtos.User;

namespace TicketSelling.Application.Dtos.Order;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public UserDto? User { get; set; }
    public PaymentDto? Payment { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
