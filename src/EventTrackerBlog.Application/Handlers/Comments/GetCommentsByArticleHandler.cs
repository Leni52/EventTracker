using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class GetCommentsByArticleHandler : IRequestHandler<GetCommentsByArticleQuery, IEnumerable<Comment>>
    {
        private readonly BlogDbContext _context;
        public GetCommentsByArticleHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCommentsByArticleQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments
                .Where(c => c.ArticleId == request.ArticleId)
                .ToListAsync(cancellationToken: cancellationToken);

            return comments;
        }
    }
}