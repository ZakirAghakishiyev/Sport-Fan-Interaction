using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class CardDetails : BaseEntity
{
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Card type is required.")]
    [MaxLength(20, ErrorMessage = "Card type cannot exceed 20 characters.")]
    public string? CardType { get; set; }

    [Required(ErrorMessage = "Last 4 digits are required.")]
    [Range(1000, 9999, ErrorMessage = "Last 4 digits must be a valid 4-digit number.")]
    public int? Last4Digits { get; set; }

    [Required(ErrorMessage = "Card number is required.")]
    [MinLength(16, ErrorMessage = "Encoded card number must be at least 16 characters.")]
    [MaxLength(64, ErrorMessage = "Encoded card number cannot exceed 64 characters.")]
    public string? All16Digits { get; set; }

    [Required(ErrorMessage = "Encoded CVC is required.")]
    [Range(100, 9999, ErrorMessage = "Encoded CVC must be 3 or 4 digits.")]
    public int? Cvc { get; set; }

    [Required(ErrorMessage = "Expiration date is required.")]
    [DataType(DataType.Date)]
    public DateTime? ExpirationDate { get; set; }

    [Required(ErrorMessage = "Card holder name is required.")]
    [MaxLength(100, ErrorMessage = "Card holder name cannot exceed 100 characters.")]
    public string? CardHolderName { get; set; }
}
