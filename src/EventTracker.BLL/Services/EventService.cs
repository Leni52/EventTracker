using EventTracker.BLL.Interfaces;
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

        public EventService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
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

            await _context.Events.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(EventRequestDTO eventRequest, Guid eventId)
        {
            var eventToUpdate = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
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

            _context.Events.Update(eventToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await GetEventByIdAsync(eventId);
            if (eventToDelete == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
