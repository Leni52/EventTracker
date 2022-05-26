using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(Event eventToCreate);
        Task DeleteEventAsync(Guid eventId);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task EditEventAsync(Event editedEvent, Guid eventId);
        Task<IEnumerable<Comment>> GetAllCommentsFromEvent(Guid eventId);
    }
}