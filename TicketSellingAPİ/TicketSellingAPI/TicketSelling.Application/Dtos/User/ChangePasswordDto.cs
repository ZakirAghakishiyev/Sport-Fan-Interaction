namespace TicketSelling.Application.Dtos.User;

public class ChangePasswordDto
{
    public string UserName { get; set; } = null!;
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
