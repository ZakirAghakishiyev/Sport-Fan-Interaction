namespace TicketSelling.Core.Entities
{
    public class Stadium: BaseEntity
    {
        public required string Name { get; set; }
        public List<Sector> Sectors { get; set; } = [];
    }
}
