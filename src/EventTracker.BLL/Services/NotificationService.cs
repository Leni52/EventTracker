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

        public async void SendNotificationAsync(Event @event)
        {
            var mailRequest = new MailRequest
            {
                ToEmail = "eventtrackermail@gmail.com",
                Subject = string.Format(CreatedEventSubject, @event.Name),
                Body = string.Format(CreatedEventBody, @event.Id, @event.Name)
            };

            await _mailService.SendEmailAsync(mailRequest);
        }
    }
}
