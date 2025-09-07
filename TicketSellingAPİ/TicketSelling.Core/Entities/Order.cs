using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class Order: BaseEntity
{
    [Required(ErrorMessage = "User is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Order date is required")]
    public DateTime OrderDate { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0")]
    public decimal TotalAmount { get; set; }

    [MinLength(1, ErrorMessage = "At least one order item is required")]
    public List<OrderItem> Items { get; set; } = [];

    public Payment? Payment { get; set; }

    public AppUser? User { get; set; }
}
