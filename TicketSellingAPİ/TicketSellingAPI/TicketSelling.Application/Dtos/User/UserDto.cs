using TicketSelling.Application.Dtos.Order;
using TicketSelling.Application.Dtos.Ticket;
using TicketSelling.Application.Dtos.UserSavedCards;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.User;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public LoyaltyTier LoyaltyTier { get; set; }

    public List<TicketDto> Tickets { get; set; } = new();
    public List<OrderDto> MerchandiseOrders { get; set; } = new();
    public List<UserSavedCardsDto> UserSavedCards { get; set; } = new();
}
