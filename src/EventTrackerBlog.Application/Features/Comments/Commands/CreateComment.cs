using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;
using MediatR;

namespace EventTrackerBlog.Application.Features.Comments.Commands
{
    public class CreateComment : IRequest<CommentResponseModel>
    {
        public string Content { get; }
        public Guid ArticleId { get; }

        public CreateComment(string content, Guid articleId)
        {
            Content = content;
            ArticleId = articleId;
        }

        public class CreateCommentHandler : IRequestHandler<CreateComment, CommentResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public CreateCommentHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommentResponseModel> Handle(CreateComment request, CancellationToken cancellationToken)
            {
                var comment = _mapper.Map<Comment>(request);
                await _context.Comments.AddAsync(comment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return _mapper.Map<CommentResponseModel>(comment);
            }
        }
    }
}