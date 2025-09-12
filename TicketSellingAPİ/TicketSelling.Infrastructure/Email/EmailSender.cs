using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace TicketSelling.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    private readonly bool _enableSsl;

    public EmailSender(IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection("SmtpSettings");
        _smtpHost = smtpSettings["Host"];
        _smtpPort = int.Parse(smtpSettings["Port"]);
        _smtpUser = smtpSettings["Username"];
        _smtpPass = smtpSettings["Password"];
        _enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
    }

    public async Task SendAsync(string toEmail, string subject, string message)
    {
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = _enableSsl
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpUser, "TicketSelling App"),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        await client.SendMailAsync(mailMessage);
    }
}
