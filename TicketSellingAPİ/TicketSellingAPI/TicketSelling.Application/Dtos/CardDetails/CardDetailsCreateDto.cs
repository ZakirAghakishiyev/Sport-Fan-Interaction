namespace TicketSelling.Application.Dtos.CardDetails;

public class CardDetailsCreateDto
{
    public int UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CardType { get; set; }
    public required string All16Digits { get; set; }
    public int Cvc { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CardHolderName { get; set; } = string.Empty;
}

