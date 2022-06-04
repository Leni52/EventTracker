using AutoMapper;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                var article = await _context.Articles
                      .Include(a => a.Comments)
                      .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken: cancellationToken);

                if (article == null)
                {
                    throw new ItemDoesNotExistException();
                }

                var reactions = new List<Reaction>();   

                foreach (var comment in article.Comments)
                {
                    _context.Entry(comment)
                        .Collection(c => c.Reactions)
                        .Load();

                    foreach (var reaction in comment.Reactions)
                    {
                        reactions.Add(reaction);
                    }
                }

                return _mapper.Map<IEnumerable<ReactionResponseModel>>(reactions);
            }
        }
    }
}