namespace TicketSelling.Core.Entities;

public class Merchandise: BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public int Stock { get; set; }
}
