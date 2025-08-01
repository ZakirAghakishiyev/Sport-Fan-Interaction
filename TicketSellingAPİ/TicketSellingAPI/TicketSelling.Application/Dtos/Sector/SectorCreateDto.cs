using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Sector
{
    public class SectorCreateDto
    {
        public int StadiumId { get; set; }
        public string Name { get; set; } = string.Empty;
        public SeatClass? SeatClass { get; set; }
    }

}
