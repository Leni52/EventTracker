using EventTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface IEventRepository
    {
        Task AddEventAsync(Event eventt);

        Task<List<Event>> GetAllEventsAsync();

        Task<Event> GetEventByIdAsync(Guid eventId);

        void RemoveEvent(Event eventt);

        Task SaveChangesAsync();

        void UpdateEvent(Event eventt);


    }
}
