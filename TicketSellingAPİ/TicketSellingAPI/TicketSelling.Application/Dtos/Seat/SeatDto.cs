using TicketSelling.Application.Dtos.Sector;

namespace TicketSelling.Application.Dtos.Seat;

public class SeatDto
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public SectorDto? Sector { get; set; }
}
