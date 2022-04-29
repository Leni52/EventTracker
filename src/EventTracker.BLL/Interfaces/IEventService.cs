using EventTracker.DAL.Entities;
using EventTracker.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventRequestDTO eventRequest);
        Task DeleteEventAsync(Guid eventId);
        Task<List<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task UpdateEventAsync(EventRequestDTO eventRequest, Guid eventId);
    }
}