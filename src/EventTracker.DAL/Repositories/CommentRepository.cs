using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsFromEventAsync(Guid eventId)
        {
            var eventNeeded = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            return eventNeeded.Comments;
        }
    }
}
