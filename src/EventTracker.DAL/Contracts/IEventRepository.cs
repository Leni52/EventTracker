using EventTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        void AddUserToEvent(Event eventData, ExternalUser user);
        Task<bool> CheckIfNameExistsCreate(string name);
        Task<bool> CheckIfNameExistsEdit(string requestName, string editName);
        bool CheckIfUserIsInEvent(Event eventData, ExternalUser user);
        void RemoveUserFromEvent(Event eventData, ExternalUser user);
    }
}