using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Comments.Queries
{
    public class GetAllComments : IRequest<IEnumerable<CommentResponseModel>>
    {
        public class GetAllCommentsHandler : IRequestHandler<GetAllComments, IEnumerable<CommentResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetAllCommentsHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CommentResponseModel>> Handle(GetAllComments request, CancellationToken cancellationToken)
            {
                var comments = await _context.Comments
                    .ToListAsync(cancellationToken: cancellationToken);

                return _mapper.Map<IEnumerable<CommentResponseModel>>(comments);
            }
        }
    }
}