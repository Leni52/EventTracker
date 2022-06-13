using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        Task SignUpRegularUserAsync(Guid eventId, ClaimsPrincipal claimsPrincipal);
        Task SignOutRegularUserAsync(Guid eventId, ClaimsPrincipal claimsPrincipal);
    }
}