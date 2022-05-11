using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
       

        public EventService(DatabaseContext context, IEventRepository eventRepository,
            IMapper mapper)
        {
            _context = context;
            _eventRepository = eventRepository;
            _mapper = mapper;            
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _eventRepository.GetByIdAsync(eventId);
        }

        public async Task CreateEventAsync(EventRequestModel eventRequest)
        {
            var eventToCreate = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventRequest.Name);
            if (eventToCreate != null)
            {
                throw new Exception("Name is already in use.");
            }

            eventToCreate = _mapper.Map<Event>(eventRequest);
            eventToCreate.CreatedAt = DateTime.Now;
            eventToCreate.LastModifiedAt = DateTime.Now;

            await _eventRepository.CreateAsync(eventToCreate);
            await _eventRepository.SaveAsync();
        }

        public async Task EditEventAsync(EventRequestModel eventRequest, Guid eventId)
        {
            var eventToEdit = await _eventRepository.GetByIdAsync(eventId);
            if (eventToEdit == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            bool checkName = await _context.Events.AnyAsync(e => e.Name == eventRequest.Name) && eventToEdit.Name != eventRequest.Name;
            if (checkName)
            {
                throw new Exception("Name is already in use.");
            }

            eventToEdit = _mapper.Map<Event>(eventRequest);
            eventToEdit.LastModifiedAt = DateTime.Now;

            _eventRepository.Update(eventToEdit);
            await _eventRepository.SaveAsync();
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await _eventRepository.GetByIdAsync(eventId);
            if (eventToDelete == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            _eventRepository.Delete(eventToDelete);
            await _eventRepository.SaveAsync();
        }
        public async Task<IEnumerable<Comment>> GetAllCommentsFromEvent(Guid eventId)
        {
            Event commentedEvent = await GetEventByIdAsync(eventId);
            return commentedEvent.Comments;
        }
    }
}
