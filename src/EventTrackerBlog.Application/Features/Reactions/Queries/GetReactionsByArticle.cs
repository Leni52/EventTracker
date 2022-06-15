using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Reactions.Queries
{
    public class GetReactionsByArticle : IRequest<IEnumerable<ReactionResponseModel>>
    {
        public Guid ArticleId { get; }

        public GetReactionsByArticle(Guid articleId)
        {
            ArticleId = articleId;
        }

        public class GetReactionByIdHandler : IRequestHandler<GetReactionsByArticle, IEnumerable<ReactionResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetReactionByIdHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ReactionResponseModel>> Handle(GetReactionsByArticle request, CancellationToken cancellationToken)
            {
                var reactions = await _context.Reactions.Where(r => r.Comment.ArticleId == request.ArticleId)
                    .ToListAsync(cancellationToken: cancellationToken);

                return _mapper.Map<IEnumerable<ReactionResponseModel>>(reactions);
            }
        }
    }
}