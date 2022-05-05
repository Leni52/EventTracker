using EventTracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
    }
}
