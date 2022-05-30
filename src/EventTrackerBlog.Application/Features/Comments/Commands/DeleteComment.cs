using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Data.Data;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Comments.Commands
{
    public class DeleteComment : IRequest<Guid>
    {
        public Guid ArticleId { get; set; }
        public Guid CommentId { get; }

        public DeleteComment(Guid articleId, Guid commentId)
        {
            ArticleId = articleId;
            CommentId = commentId;
        }

        public class DeleteCommentHandler : IRequestHandler<DeleteComment, Guid>
        {
            private readonly BlogDbContext _context;

            public DeleteCommentHandler(BlogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteComment request, CancellationToken cancellationToken)
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
}