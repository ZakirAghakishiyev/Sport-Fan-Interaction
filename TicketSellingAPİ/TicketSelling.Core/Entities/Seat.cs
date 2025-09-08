using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Seat: BaseEntity
{
    [Required]
    public int SectorId { get; set; }

    [Required(ErrorMessage = "Row number is required")]
    [Range(1, 200, ErrorMessage = "Row number must be between 1 and 200")]
    public int Row { get; set; }

    [Required(ErrorMessage = "Seat number is required")]
    [Range(1, 500, ErrorMessage = "Seat number must be between 1 and 500")]
    public int Number { get; set; }

    public Sector? Sector { get; set; }
}
