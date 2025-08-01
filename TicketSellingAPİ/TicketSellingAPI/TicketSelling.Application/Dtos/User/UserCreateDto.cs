using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.User;

public class UserCreateDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier LoyaltyTier { get; set; }
}
