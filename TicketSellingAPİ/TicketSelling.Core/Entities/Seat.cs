namespace TicketSelling.Core.Entities;

public class Seat: BaseEntity
{
    public int SectorId { get; set; }
    public int Row { get; set; }
    public int Number { get; set; }
    public Sector? Sector { get; set; }
}
