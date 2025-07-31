namespace TicketSelling.Core.Entities
{
    public class Match: BaseEntity
    {
        public DateTime Date { get; set; }
        public required string Opponent { get; set; }
        public int StadiumId { get; set; }

        public Stadium? Stadium { get; set; }
        public List<MatchSectorPrice> SectorPrices { get; set; } = [];
    }


}
