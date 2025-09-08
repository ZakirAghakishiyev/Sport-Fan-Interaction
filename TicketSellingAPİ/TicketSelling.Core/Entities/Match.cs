using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Match: BaseEntity
{
    [Required(ErrorMessage = "Match date is required")]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Opponent name is required")]
    [StringLength(150, ErrorMessage = "Opponent name cannot exceed 150 characters")]
    public required string Opponent { get; set; }

    [Required]
    public int StadiumId { get; set; }

    public Stadium? Stadium { get; set; }
    public List<MatchSectorPrice> SectorPrices { get; set; } = [];
}
