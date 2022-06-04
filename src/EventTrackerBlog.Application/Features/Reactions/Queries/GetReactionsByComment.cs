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
    public class GetReactionsByComment : IRequest<IEnumerable<ReactionResponseModel>>
    {
        public Guid CommentId { get; }

        public GetReactionsByComment(Guid commentId)
        {
            CommentId = commentId;
        }

        public class GetReactionByIdHandler : IRequestHandler<GetReactionsByComment, IEnumerable<ReactionResponseModel>>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetReactionByIdHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ReactionResponseModel>> Handle(GetReactionsByComment request, CancellationToken cancellationToken)
            {
                var comment = await _context.Comments
                    .Include(c => c.Reactions)
                    .FirstOrDefaultAsync(a => a.Id == request.CommentId, cancellationToken: cancellationToken);

                if (comment == null)
                {
                    throw new ItemDoesNotExistException();
                }

                var reactions = new List<Reaction>();

                foreach (var reaction in comment.Reactions)
                {
                        reactions.Add(reaction);
                }

                return _mapper.Map<IEnumerable<ReactionResponseModel>>(reactions);
            }
        }
    }
}
