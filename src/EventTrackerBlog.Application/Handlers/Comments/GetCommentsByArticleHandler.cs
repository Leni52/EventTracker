using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventTrackerBlog.Application.Exceptions;
using EventTrackerBlog.Domain.DTO.Comments.Response;

namespace EventTrackerBlog.Application.Handlers.Comments
{
    public class GetCommentsByArticleHandler : IRequestHandler<GetCommentsByArticleQuery, IEnumerable<CommentResponseModel>>
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentsByArticleHandler(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponseModel>> Handle(GetCommentsByArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken: cancellationToken);

            if (article == null)
            {
                throw new ItemDoesNotExistException();
            }

            var comments = article
                .Comments.ToList();

            return _mapper.Map<IEnumerable<CommentResponseModel>>(comments);
        }
    }
}