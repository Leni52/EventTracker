using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
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

        public async void SendNotificationAsync(Event targetEvent)
        {
            var mailRequest = new MailRequest
            {
                ToEmail = "eventtrackermail@gmail.com",
                Subject = string.Format(CreatedEventSubject, targetEvent.Name),
                Body = string.Format(CreatedEventBody, targetEvent.Id, targetEvent.Name)
            };

            await _mailService.SendEmailAsync(mailRequest);
        }
    }
}