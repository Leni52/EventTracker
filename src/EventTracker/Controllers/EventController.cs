﻿using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.Requests;
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

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(EventRequestDTO eventRequest)
        {
            await _eventService.CreateEventAsync(eventRequest);
            if (ModelState.IsValid)
            {
                return Ok("Event created successfully.");
            }

            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateEventAsync(EventRequestDTO eventRequest, Guid eventId)
        {
            await _eventService.UpdateEventAsync(eventRequest, eventId);
            if (ModelState.IsValid)
            {
                return Ok("Event updated successfully.");
            }

            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEventAsync(Guid eventId)
        {
            await _eventService.DeleteEventAsync(eventId);
            if (ModelState.IsValid)
            {
                return Ok("Event deleted successfully.");
            }

            return BadRequest();
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
