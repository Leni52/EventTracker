using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Commands.Comments;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Domain.Data;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Guid>
    {
        private readonly BlogDbContext _context;

        public DeleteCommentHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _context.Comments
                .FirstOrDefault(c => c.Id == request.CommentId);

            if (comment == null)
            {
                throw new ItemDoesNotExistException();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken: cancellationToken);

            return comment.Id;
        }
    }
}
