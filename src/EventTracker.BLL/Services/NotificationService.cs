using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;

namespace EventTracker.BLL.Services
{
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
                Subject = $"Created event {@event.Name}",
                Body = $"You successfully created an event {@event.Id} - {@event.Name}"
            };

            await _mailService.SendEmailAsync(mailRequest);
        }
    }
}
