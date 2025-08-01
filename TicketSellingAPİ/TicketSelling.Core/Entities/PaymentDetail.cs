namespace TicketSelling.Core.Entities;

public class PaymentDetail : BaseEntity
{
    public int PaymentId { get; set; }
    public int CardDetailsId { get; set; }
    public string? BillingAddress { get; set; }
    public string? Email { get; set; }
    public string? Note { get; set; }
    public Payment? Payment { get; set; }
    public CardDetails? CardDetails { get; set; }
}


