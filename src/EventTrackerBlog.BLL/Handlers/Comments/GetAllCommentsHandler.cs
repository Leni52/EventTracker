using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.BLL.Queries.Comments;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.BLL.Handlers.Comments
{
    public class GetAllCommentsHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<Comment>>
    {
        private readonly BlogDbContext _context;
        public GetAllCommentsHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments
                .Where(c => c.ArticleId == request.ArticleId)
                .ToListAsync(cancellationToken: cancellationToken);

            return comments;
        }
    }
}
