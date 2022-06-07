﻿using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Entities;
using EventTracker.DTO.CommentModels;
using EventTracker.DTO.EventModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private static IEventService _eventService;
        private static IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper) : base()
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "RegularUser")]
        public async Task<IEnumerable<EventResponseModel>> GetAllEventsAsync()
        {
            var events = await _eventService.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventResponseModel>>(events);
        }

        [HttpGet("{eventId}")]
        // [Authorize(Roles = "RegularUser")]
        public async Task<EventResponseModel> GetEventByIdAsync(Guid eventId)
        {
            var eventById = await _eventService.GetEventByIdAsync(eventId);
            return _mapper.Map<EventResponseModel>(eventById);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin, EventHolder")]
        public async Task<IActionResult> CreateEventAsync(EventRequestModel eventRequest)
        {
            var eventToCreate = _mapper.Map<Event>(eventRequest);

            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventToCreate);
                return Ok("Event created successfully.");
            }

            return BadRequest();
        }

        [HttpPut("{eventId}")]
        //[Authorize(Roles = "Admin, EventHolder")]
        public async Task<IActionResult> EditEventAsync(EventRequestModel eventRequest, Guid eventId)
        {
            var editedEvent = _mapper.Map<Event>(eventRequest);

            if (ModelState.IsValid)
            {
                await _eventService.EditEventAsync(editedEvent, eventId);
                return Ok("Event updated successfully.");
            }

            return BadRequest();
        }

        [HttpDelete("{eventId}")]
        // [Authorize(Roles = "Admin, EventHolder")]
        public async Task<IActionResult> DeleteEventAsync(Guid eventId)
        {
            await _eventService.DeleteEventAsync(eventId);
            if (ModelState.IsValid)
            {
                return Ok("Event deleted successfully.");
            }

            return BadRequest();
        }

        [HttpGet("{eventId}/comments")]
        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsFromEvent(Guid eventId)
        {
            var comments = await _eventService.GetAllCommentsFromEvent(eventId);

            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }

        [HttpPut("{eventId}/SignUp")]
        [Authorize(Roles = "RegularUser")]
        public async Task<IActionResult> SignUpForEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.SignUpRegularUserAsync(eventId, User);

            return Ok();
        }
    }
}