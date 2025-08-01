namespace TicketSelling.Core.Entities;

public class CardDetails : BaseEntity
{
    public string? PhoneNumber { get; set; }
    public string? CardType { get; set; }
    public int? Last4Digits { get; set; }
    public int? Encoded16Digits { get; set; }
    public int? EncodedCvc { get; set; }
    public DateTime? EncodedExpirationDate { get; set; }
    public string? EncodedCardHolderName { get; set; }
}


