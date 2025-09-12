﻿namespace TicketSelling.Infrastructure.Email;

public interface IEmailSender
{
    Task SendAsync(string toEmail, string subject, string message);
}