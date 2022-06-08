using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventTrackerBlog.Domain.Features.Comments.Queries
{
    public class GetCommentById : IRequest<CommentResponseModel>
    {
        public Guid ArticleId { get; }
        public Guid CommentId { get; }

        public GetCommentById(Guid articleId, Guid commentId)
        {
            ArticleId = articleId;
            CommentId = commentId;
        }

        public class GetCommentByIdHandler : IRequestHandler<GetCommentById, CommentResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetCommentByIdHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CommentResponseModel> Handle(GetCommentById request, CancellationToken cancellationToken)
            {
                var article = await _context.Articles
                    .Include(x => x.Comments)
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

                return _mapper.Map<CommentResponseModel>(comment);
            }
        }
    }
}