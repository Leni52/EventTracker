using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext _context;
        private readonly IEventRepository _eventRepository;

        public EventService(DatabaseContext context, IEventRepository eventRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _eventRepository.GetByIdAsync(eventId);
        }

        public async Task CreateEventAsync(EventRequestDTO eventRequest)
        {
            var eventToCreate = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventRequest.Name);
            if (eventToCreate != null)
            {
                throw new Exception("Name is already in use.");
            }

            eventToCreate = new Event()
            {
                Name = eventRequest.Name,
                Description = eventRequest.Description,
                Category = eventRequest.Category,
                Location = eventRequest.Location,
                StartDate = eventRequest.StartDate,
                EndDate = eventRequest.EndDate,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now
            };

            await _eventRepository.CreateAsync(eventToCreate);
            await _eventRepository.SaveAsync();
        }

        public async Task UpdateEventAsync(EventRequestDTO eventRequest, Guid eventId)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(eventId);
            if (eventToUpdate == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            bool checkName = await _context.Events.AnyAsync(e => e.Name == eventRequest.Name) && eventToUpdate.Name != eventRequest.Name;
            if (checkName)
            {
                throw new Exception("Name is already in use.");
            }

            eventToUpdate.Name = eventRequest.Name;
            eventToUpdate.Description = eventRequest.Description;
            eventToUpdate.Category = eventRequest.Category;
            eventToUpdate.Location = eventRequest.Location;
            eventToUpdate.StartDate = eventRequest.StartDate;
            eventToUpdate.EndDate = eventRequest.EndDate;
            eventToUpdate.LastModifiedAt = DateTime.Now;

            _eventRepository.Update(eventToUpdate);
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
    }
}
