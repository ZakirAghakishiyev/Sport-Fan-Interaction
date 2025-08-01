namespace TicketSelling.Core.Entities;

public class Sector: BaseEntity
{
    public int StadiumId { get; set; }
    public required string Name { get; set; }
    public SeatClass? SeatClass { get; set; }

    public Stadium? Stadium { get; set; }
    public List<Seat> Seats { get; set; } = [];
}

