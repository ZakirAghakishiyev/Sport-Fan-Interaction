using TicketSelling.Core.Entities;

namespace TicketSelling.Application.Dtos.User;

public class UserUpdateDto
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}

