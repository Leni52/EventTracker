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
    public class EventService
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
            var eventToCreate = new Event()
            {
                Name = eventRequest.Name,
                Description = eventRequest.Description,
                Location = eventRequest.Location,
                StartDate = eventRequest.StartDate,
                EndDate = eventRequest.EndDate,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now
            };

            await _context.Events.AddAsync(eventToCreate);
            await _context.SaveChangesAsync();
        }
    }
}
