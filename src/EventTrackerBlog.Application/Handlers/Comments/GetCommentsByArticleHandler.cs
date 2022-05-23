using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            var comments = await _context.Comments
                .Where(c => c.ArticleId == request.ArticleId)
                .ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<CommentResponseModel>>(comments);
        }
    }
}