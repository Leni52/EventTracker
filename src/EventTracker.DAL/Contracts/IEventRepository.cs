﻿using EventTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<bool> CheckIfNameExistsCreate(string name);
        Task<bool> CheckIfNameExistsEdit(string requestName, string editName);
    }
}
