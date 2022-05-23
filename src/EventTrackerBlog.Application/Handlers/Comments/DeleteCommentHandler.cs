using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var article = await _context.Articles
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken: cancellationToken);

            if (article == null)
            {
                throw new ItemDoesNotExistException();
            }

            var comment = article.Comments
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