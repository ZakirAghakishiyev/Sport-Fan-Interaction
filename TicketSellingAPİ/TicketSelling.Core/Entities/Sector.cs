using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Sector: BaseEntity
{
    [Required]
    public int StadiumId { get; set; }

    [Required(ErrorMessage = "Sector name is required")]
    [StringLength(100, ErrorMessage = "Sector name cannot exceed 100 characters")]
    public required string Name { get; set; }
    public SeatClass? SeatClass { get; set; }

    public Stadium? Stadium { get; set; }
    public List<Seat> Seats { get; set; } = [];
}

