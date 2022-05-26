using EventTracker.DAL.Entities;

namespace EventTracker.BLL.Interfaces
{
    public interface INotificationService
    {
        void SendNotificationAsync(Event targetEvent, string subject, string body);
    }
}