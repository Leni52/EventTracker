using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentResponseModel>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public CreateCommentHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentResponseModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CommentResponseModel>(comment);
        }
    }
}