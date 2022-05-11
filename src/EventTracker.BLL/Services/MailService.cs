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
        private readonly Entities.MailSettings mailSettings;

        public MailService(IOptions<Entities.MailSettings> options)
        {
            this.mailSettings = options.Value;
        }

        public async Task SendMail(MailRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail)); 
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
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
