using AutoMapper;
using EventTrackerBlog.Domain.Common;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;
using ExceptionHandling.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Reactions.Commands
{
    public class CreateReaction :IRequest<ReactionResponseModel>
    {
        public Guid CommentId { get; }
        public ReactionType Type { get; }

        public CreateReaction(Guid commentId, ReactionType type)
        {
            CommentId = commentId;
            Type = type;
        }

        public class CreateReactionHandler : IRequestHandler<CreateReaction, ReactionResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public CreateReactionHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ReactionResponseModel> Handle(CreateReaction request, CancellationToken cancellationToken)
            {
                var comment = _context.Comments.FirstOrDefault(c => c.Id == request.CommentId);

                if (comment == null)
                {
                    throw new ItemDoesNotExistException();
                }

                var reaction = new Reaction()
                {
                    CommentId = request.CommentId,
                    Type = request.Type,
                    CreatedAt = DateTime.Now,
                    LastModifiedAt = DateTime.Now
                };

                await _context.Reactions.AddAsync(reaction, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<ReactionResponseModel>(reaction);
            }
        }
    }
}
