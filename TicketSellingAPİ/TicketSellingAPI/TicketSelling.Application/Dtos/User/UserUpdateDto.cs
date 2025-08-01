using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.User;

public class UserUpdateDto 
{
    public string? Email { get; set; }
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier LoyaltyTier { get; set; }
}
