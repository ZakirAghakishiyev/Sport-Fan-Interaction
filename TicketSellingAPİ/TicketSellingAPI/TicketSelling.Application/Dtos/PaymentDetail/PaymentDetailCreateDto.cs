namespace TicketSelling.Application.Dtos.PaymentDetail;

public class PaymentDetailCreateDto
{
    public int PaymentId { get; set; }
    public int CardDetailsId { get; set; }
    public string? BillingAddress { get; set; }
    public string? Email { get; set; }
    public string? Note { get; set; }
}

