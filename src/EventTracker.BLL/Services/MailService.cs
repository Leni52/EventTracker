using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EventTracker.BLL.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService(IOptions<MailSettings> options)
        {
            this.mailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailSettings.Mail)
            };

            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail)); 
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            smtp.Dispose();
        }
    }
}
