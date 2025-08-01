using TicketSelling.Application.Dtos.Order;
using TicketSelling.Application.Dtos.PaymentDetail;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Payment;

public class PaymentDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
    public string? TransactionId { get; set; }

    public PaymentDetailDto? PaymentDetail { get; set; }
    public OrderDto? Order { get; set; }
}
