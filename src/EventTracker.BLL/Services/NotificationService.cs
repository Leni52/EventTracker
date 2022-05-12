using EventTracker.BLL.Entities;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using MimeKit;

namespace EventTracker.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMailService _mailService;

        public NotificationService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public void SendNotification(Event @event)
        {
            var mailRequest = new MailRequest();

            mailRequest.ToEmail = "ativassileva@gmail.com";
            mailRequest.Subject = $"Created event {@event.Name}";
            mailRequest.Body = $"You successfully created an event {@event.Id} - {@event.Name}"; 

            _mailService.SendEmailAsync(mailRequest);
        }
    }
}
