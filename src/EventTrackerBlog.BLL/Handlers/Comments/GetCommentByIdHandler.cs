using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Queries.Comments;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, Comment>
    {
        private readonly BlogDbContext _context;

        public GetCommentByIdHandler(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            return comment;
        }
    }
}
