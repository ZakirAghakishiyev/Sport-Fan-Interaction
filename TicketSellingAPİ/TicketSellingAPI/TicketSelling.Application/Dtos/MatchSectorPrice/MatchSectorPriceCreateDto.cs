namespace TicketSelling.Application.Dtos.MatchSectorPrice
{
    public class MatchSectorPriceCreateDto
    {
        public int MatchId { get; set; }
        public int SectorId { get; set; }
        public decimal Price { get; set; }
    }

}
