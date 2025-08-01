namespace TicketSelling.Core.Entities;

public class UserSavedCards:BaseEntity
{
    public int UserId { get; set; }
    public int CardDetailsId { get; set; }
    public User? User { get; set; }
    public CardDetails? CardDetails { get; set; }
}
