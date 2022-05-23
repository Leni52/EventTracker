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
        Task CreateEventAsync(EventRequestModel eventRequest);
        Task DeleteEventAsync(Guid eventId);
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task EditEventAsync(EventRequestModel eventRequest, Guid eventId);
        Task<IEnumerable<Comment>> GetAllCommentsFromEvent(Guid eventId);
        Task SignUpRegularUser(Guid eventId, ClaimsPrincipal claimsPrincipal);
    }
}