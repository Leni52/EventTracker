using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Repositories
{
    class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;

        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddEventAsync(Event eventt)
        {
            await _context.Events.AddAsync(eventt);
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public void RemoveEvent(Event eventt)
        {
            _context.Events.Remove(eventt);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateEvent(Event eventt)
        {
            _context.Events.Update(eventt);
        }
    }
}
