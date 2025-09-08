using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class PaymentDetail : BaseEntity
{
    [Required]
    public int PaymentId { get; set; }

    [Required]
    public int CardDetailsId { get; set; }

    [Required(ErrorMessage = "Billing address is required")]
    [StringLength(200, ErrorMessage = "Billing address cannot exceed 200 characters")]
    public string? BillingAddress { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string? Email { get; set; }

    [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters")]
    public string? Note { get; set; }

    // Navigation properties
    public Payment? Payment { get; set; }
    public CardDetails? CardDetails { get; set; }
}


