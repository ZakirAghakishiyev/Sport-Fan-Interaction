namespace TicketSelling.Application.Dtos.Ticket
{
    public class TicketCreateDto
    {
        public int MatchId { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public bool IsGiftedUpgrade { get; set; }
    }

}
