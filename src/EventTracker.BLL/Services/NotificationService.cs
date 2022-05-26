using System.Linq;
using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
using EventTracker.BLL.Models;
using EventTracker.DAL.Entities;

namespace EventTracker.BLL.Services
{
    using static Common.NotificationMessages;

    public class NotificationService : INotificationService
    {
        private readonly IMailService _mailService;

        public NotificationService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async void SendNotificationAsync(Event targetEvent, string subject, string body)
        {
            var mailRequest = new MailRequest
            {
                Recipients = targetEvent.Users
                    .Select(x => x.EmailAddress)
                    .ToList(),
                Subject = subject,
                Body = body
            };

            await _mailService.SendEmailAsync(mailRequest);
        }
    }
}