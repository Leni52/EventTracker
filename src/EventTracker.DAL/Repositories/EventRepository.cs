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
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(DatabaseContext context) : base(context)
        {
        }

        public void AddUserToEvent(Event eventData, ExternalUser user)
        {
            eventData.Users.Add(user);
        }

        public void RemoveUserFromEvent(Event eventData, ExternalUser user)
        {
            eventData.Users.Remove(user);
        }

        public bool CheckIfUserIsInEvent(Event eventData, ExternalUser user)
        {
            return eventData.Users.Contains(user);
        }

        public async Task<bool> CheckIfNameExistsCreate(string name)
        {
            return await _context.Events.AnyAsync(e => e.Name == name);
        }

        public async Task<bool> CheckIfNameExistsEdit(string requestName, string editName)
        {
            return await _context.Events.AnyAsync(e => e.Name == requestName) && editName != requestName;
        }
    }
}