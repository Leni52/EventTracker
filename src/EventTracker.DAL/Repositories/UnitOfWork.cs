using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Repositories
{
    public class UnitOfWork
    {
        private readonly DatabaseContext _context;
        private IEventRepository _eventRepository;
        private ICommentRepository _commentRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IEventRepository Events
            => _eventRepository ??= new EventRepository(_context);
        public ICommentRepository Comments
            => _commentRepository ??= new CommentRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
