using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Dtos.Payment;

namespace TicketSelling.Application.Dtos.PaymentDetail;

public class PaymentDetailDto
{
    public int Id { get; set; }
    public string? BillingAddress { get; set; }
    public string? Email { get; set; }
    public string? Note { get; set; }

    public CardDetailsDto? CardDetails { get; set; }
    public PaymentDto? Payment { get; set; }
}


