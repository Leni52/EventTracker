using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.BLL.Queries.Comments;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.BLL.Handlers.Comments
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
