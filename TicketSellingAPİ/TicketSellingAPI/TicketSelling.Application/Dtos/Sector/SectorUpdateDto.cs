using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.Sector
{
    public class SectorUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public SeatClass? SeatClass { get; set; }
    }

}
