namespace TicketSelling.Application.Dtos.User;

public class ResetPasswordDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;   // comes from email link or external provider
    public string NewPassword { get; set; } = null!;
}
