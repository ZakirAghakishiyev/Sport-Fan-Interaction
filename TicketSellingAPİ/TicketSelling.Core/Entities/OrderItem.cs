using System.ComponentModel.DataAnnotations;

namespace TicketSelling.Core.Entities;

public class OrderItem: BaseEntity
{
    [Required(ErrorMessage = "OrderId is required")]
    public int OrderId { get; set; }

    [Required(ErrorMessage = "MerchandiseId is required")]
    public int MerchandiseId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price at purchase must be greater than 0")]
    public decimal PriceAtPurchase { get; set; }

    public Merchandise? Merchandise { get; set; }
    public Order? Order { get; set; }
}
