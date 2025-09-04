﻿namespace TicketSelling.Application.Dtos.User;

public class RegisterDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = null!;
}
