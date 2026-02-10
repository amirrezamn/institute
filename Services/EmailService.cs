using System.Net;
using System.Net.Mail;
using institute.Interfaces;

namespace institute.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtp = _config["Email:SmtpServer"];
        var port = int.Parse(_config["Email:Port"]!);
        var senderEmail = _config["Email:SenderEmail"];
        var senderName = _config["Email:SenderName"];
        var username = _config["Email:Username"];
        var password = _config["Email:Password"];

        var client = new SmtpClient(smtp, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(senderEmail!, senderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(to);

        await client.SendMailAsync(mail);
    }
}
