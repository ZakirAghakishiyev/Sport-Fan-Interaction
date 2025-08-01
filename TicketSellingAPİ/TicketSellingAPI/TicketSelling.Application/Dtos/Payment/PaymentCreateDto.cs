using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Payment;

public class PaymentCreateDto
{
    public int OrderId { get; set; }
    public int PaymentDetailId { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }

    public string? TransactionId { get; set; }
}
