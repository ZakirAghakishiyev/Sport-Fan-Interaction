namespace TicketSelling.Core.Entities;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int PaymentDetailId { get; set; }
    public PaymentDetail? PaymentDetail { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }

    public string? TransactionId { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded,
    Cancelled
}

public enum PaymentMethod
{
    CreditCard,
    DebitCard,
    PayPal,
    ApplePay,
    GooglePay,
    BankTransfer,
    Cash
}


