namespace TicketSelling.Application.Dtos.CardDetails;

public class CardDetailsDetailDto
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CardType { get; set; }
    public int? Last4Digits { get; set; }
    public DateTime? EncodedExpirationDate { get; set; }
    public string? EncodedCardHolderName { get; set; }
}
