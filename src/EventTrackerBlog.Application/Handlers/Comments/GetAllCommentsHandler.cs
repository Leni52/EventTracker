using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Application.Handlers.Comments
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
                .ToListAsync(cancellationToken: cancellationToken);

            return comments;
        }
    }
}
