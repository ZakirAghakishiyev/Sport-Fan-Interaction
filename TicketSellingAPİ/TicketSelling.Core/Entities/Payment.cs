using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Payment : BaseEntity
{
    [Required(ErrorMessage = "OrderId is required")]
    public int OrderId { get; set; }

    public Order? Order { get; set; }
    public PaymentDetail? PaymentDetail { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Payment date is required")]
    public DateTime PaymentDate { get; set; }

    [Required(ErrorMessage = "Payment status is required")]
    public PaymentStatus Status { get; set; }

    [Required(ErrorMessage = "Payment method is required")]
    public PaymentMethod Method { get; set; }

    [StringLength(100, ErrorMessage = "Transaction ID cannot exceed 100 characters")]
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


