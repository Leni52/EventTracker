using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly SmtpClient _smtpClient;

        public MailService(IOptions<MailSettings> options, SmtpClient smtpClient)
        {
            _mailSettings = options.Value;
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };

            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail)); 
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };

            email.Body = builder.ToMessageBody();

            _smtpClient.Connect(_mailSettings.Host, _mailSettings.Port, false);

            await _smtpClient.SendAsync(email);
            _smtpClient.Disconnect(true);
        }
    }
}