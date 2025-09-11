using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Application.Dtos.Ticket
{
    public class TicketCreateDto
    {
        [Required]
        public int MatchId { get; set; }

        [Required]
        public int SeatId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ticket price is required")]
        [Range(0.01, 10000, ErrorMessage = "Ticket price must be greater than 0")]
        public decimal Price { get; set; }

        public bool IsGiftedUpgrade { get; set; }
    }

}
