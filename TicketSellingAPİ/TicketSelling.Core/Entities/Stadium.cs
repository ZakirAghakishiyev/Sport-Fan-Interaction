using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Stadium: BaseEntity
{
    [Required(ErrorMessage = "Stadium name is required")]
    [StringLength(150, ErrorMessage = "Stadium name cannot exceed 150 characters")]
    public required string Name { get; set; }
    public List<Sector> Sectors { get; set; } = [];
}
