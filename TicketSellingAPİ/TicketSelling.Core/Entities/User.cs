using Microsoft.AspNetCore.Identity;

namespace TicketSelling.Core.Entities;

public class User : IdentityUser<int>
{
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier LoyaltyTier { get; set; }

    public List<Ticket> Tickets { get; set; } = [];
    public List<Order> MerchandiseOrders { get; set; } = [];
    public List<UserSavedCards> UserSavedCards { get; set; } = [];

}
