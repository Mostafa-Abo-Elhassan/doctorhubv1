using Domain.Interfaces;
using MailKit.Net.Smtp; // Add this for SmtpClient
using MailKit.Security; // Add this for SecureSocketOptions
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Implement
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                emailSettings["SenderName"],
                emailSettings["SenderEmail"]
            ));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(
                emailSettings["SmtpServer"],
                int.Parse(emailSettings["SmtpPort"]),
                SecureSocketOptions.StartTls // Use the MailKit.Security namespace
            );

            await client.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["Password"]
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}