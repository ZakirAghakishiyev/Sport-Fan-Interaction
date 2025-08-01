namespace TicketSelling.Application.Dtos.CardDetails;

public class CardDetailsCreateDto
{
    public string? PhoneNumber { get; set; }
    public string? CardType { get; set; }
    public int Last4Digits { get; set; }
    public int All16Digits { get; set; }
    public int Cvc { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CardHolderName { get; set; } = string.Empty;
}

