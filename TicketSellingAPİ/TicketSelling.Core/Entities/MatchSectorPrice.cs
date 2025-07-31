namespace TicketSelling.Core.Entities
{
    public class MatchSectorPrice: BaseEntity
    {
        public int MatchId { get; set; }
        public int SectorId { get; set; }
        public decimal Price { get; set; }
        public Match? Match { get; set; }
        public Sector? Sector { get; set; }
    }


}
