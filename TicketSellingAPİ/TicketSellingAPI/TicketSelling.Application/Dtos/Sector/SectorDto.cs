using System.Runtime.CompilerServices;
using TicketSelling.Application.Dtos.Seat;
using TicketSelling.Application.Dtos.Stadium;
using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Sector;

public class SectorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SeatClass? SeatClass { get; set; }

    public required string StadiumName { get; set; }
    public List<SeatDto> Seats { get; set; } = [];
}
