using EventTrackerBlog.Domain.Common;
using EventTrackerBlog.Domain.Data;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Reactions.Commands
{
    public class DeleteReaction : IRequest<Guid>
    {
        public Guid ReactionId { get; }

        public DeleteReaction(Guid reactionId)
        {
            ReactionId = reactionId;
        }

        public class DeleteReactionHandler : IRequestHandler<DeleteReaction, Guid>
        {
            private readonly BlogDbContext _context;

            public DeleteReactionHandler(BlogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteReaction request, CancellationToken cancellationToken)
            {
                var reaction = _context.Reactions.FirstOrDefault(r => r.Id == request.ReactionId);

                if (reaction == null)
                {
                    throw new ItemDoesNotExistException();
                }

                _context.Reactions.Remove(reaction);
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);

                return reaction.Id;
            }
        }
    }
}