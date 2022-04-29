using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventsController : ControllerBase
    {
        private static IEventService _eventService;

        public EventsController(IEventService eventService) : base()
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReponseDTO>>> GetAllEventsAsync()
        {
            var events = await _eventService.GetAllEventsAsync();
            var eventsResponse = new List<EventReponseDTO>();
            foreach (var e in events)
            {
                eventsResponse.Add(MapEvent(e));
            }

            return eventsResponse;
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventReponseDTO>> GetEventByIdAsync(Guid eventId)
        {
            var eventById = await _eventService.GetEventByIdAsync(eventId);

            return MapEvent(eventById);
        }

        private EventReponseDTO MapEvent(Event eventEntity)
        {
            var eventMap = new EventReponseDTO()
            {
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                Location = eventEntity.Location,
                Category = eventEntity.Category,
                StartDate = eventEntity.StartDate,
                EndDate = eventEntity.EndDate,
            };

            return eventMap;
        }
    }
}
