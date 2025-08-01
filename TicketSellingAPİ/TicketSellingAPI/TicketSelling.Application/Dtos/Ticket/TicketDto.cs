using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Dtos.User;

namespace TicketSelling.Application.Dtos.Ticket;

public class TicketDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public bool IsGiftedUpgrade { get; set; }

    public MatchDto? Match { get; set; }
    public SeatDto? Seat { get; set; }
    public UserDto? User { get; set; }
}
