using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Payment;

public class PaymentUpdateDto
{
    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
    public string? TransactionId { get; set; }
}
