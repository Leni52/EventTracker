using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
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
        private static IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper) : base()
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EventResponseModel>> GetAllEventsAsync()
        {
            var events = await _eventService.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventResponseModel>>(events);
        }

        [HttpGet("{eventId}")]
        public async Task<EventResponseModel> GetEventByIdAsync(Guid eventId)
        {
            var eventById = await _eventService.GetEventByIdAsync(eventId);
            return _mapper.Map<EventResponseModel>(eventById);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync(EventRequestModel eventRequest)
        {
            await _eventService.CreateEventAsync(eventRequest);
            if (ModelState.IsValid)
            {
                return Ok("Event created successfully.");
            }

            return BadRequest();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> EditEventAsync(EventRequestModel eventRequest, Guid eventId)
        {
            await _eventService.EditEventAsync(eventRequest, eventId);
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
    }
}
