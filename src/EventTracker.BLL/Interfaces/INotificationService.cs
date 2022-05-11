using EventTracker.DAL.Entities;

namespace EventTracker.BLL.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(Event @event);
    }
}
