using TicketSelling.Application.Dtos.Match;
using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Dtos.User;

namespace TicketSelling.Application.Dtos.Ticket;

public class TicketDto
{
     public int Id { get; set; }
    public decimal Price { get; set; }
    public bool IsGiftedUpgrade { get; set; }

    public MatchSummaryDto? Match { get; set; }
    public SeatSummaryDto? Seat { get; set; }
    public UserSummaryDto? User { get; set; }
}

public class MatchSummaryDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Opponent { get; set; } = string.Empty;
    public StadiumSummaryDto? Stadium { get; set; }
}

public class StadiumSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class SeatSummaryDto
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public SectorSummaryDto? Sector { get; set; }
}

public class SectorSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UserSummaryDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public string LoyaltyTier { get; set; } = string.Empty;
}