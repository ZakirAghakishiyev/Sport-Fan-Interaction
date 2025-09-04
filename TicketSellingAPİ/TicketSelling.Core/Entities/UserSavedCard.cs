namespace TicketSelling.Core.Entities;

public class UserSavedCard:BaseEntity
{
    public int UserId { get; set; }
    public int CardDetailsId { get; set; }
    public AppUser? User { get; set; }
    public CardDetails? CardDetails { get; set; }
}
