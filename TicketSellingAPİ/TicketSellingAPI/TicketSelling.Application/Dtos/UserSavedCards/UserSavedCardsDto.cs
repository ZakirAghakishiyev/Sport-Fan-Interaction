using TicketSelling.Application.Dtos.CardDetails;
using TicketSelling.Application.Dtos.User;

namespace TicketSelling.Application.Dtos.UserSavedCards;

public class UserSavedCardsDto
{
    public int Id { get; set; }
    public UserDto? User { get; set; }
    public CardDetailsDto? CardDetails { get; set; }
}

