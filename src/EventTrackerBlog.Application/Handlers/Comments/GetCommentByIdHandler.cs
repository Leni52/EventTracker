using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Domain.DTO.Comments.Response;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, CommentResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentByIdHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentResponseModel> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

            if (comment == null)
            {
                throw new ItemDoesNotExistException();
            }

            return _mapper.Map<CommentResponseModel>(comment);
        }
    }
}
