using TicketSelling.Application.Dtos.Sector;

namespace TicketSelling.Application.Dtos.Stadium;

public class StadiumDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<SectorDto> Sectors { get; set; } = [];
}

