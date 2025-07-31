namespace TicketSelling.Core.Entities
{
    public class Ticket: BaseEntity
    {
        public int MatchId { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public bool IsGiftedUpgrade { get; set; }
        public Match? Match { get; set; }
        public Seat? Seat { get; set; }
        public User? User { get; set; }
    }


}
