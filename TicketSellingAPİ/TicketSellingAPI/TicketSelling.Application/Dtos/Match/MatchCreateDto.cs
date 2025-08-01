namespace TicketSelling.Application.Dtos.Match
{
    public class MatchCreateDto
    {
        public DateTime Date { get; set; }
        public string Opponent { get; set; } = string.Empty;
        public int StadiumId { get; set; }
    }

}
