using Microsoft.AspNetCore.Identity;

namespace TicketSelling.Core.Entities;

public class AppUser : IdentityUser<int>
{
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier LoyaltyTier { get; set; }

    public List<Ticket> Tickets { get; set; } = [];
    public List<Order> MerchandiseOrders { get; set; } = [];
    public List<UserSavedCard> UserSavedCards { get; set; } = [];
    public List<GiftReward> GiftRewards { get; set; } = [];

}
