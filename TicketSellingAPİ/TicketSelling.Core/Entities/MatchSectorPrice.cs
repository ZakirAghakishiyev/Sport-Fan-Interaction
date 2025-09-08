using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class MatchSectorPrice: BaseEntity
{
    [Required]
    public int MatchId { get; set; }

    [Required]
    public int SectorId { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    public Match? Match { get; set; }
    public Sector? Sector { get; set; }
}
