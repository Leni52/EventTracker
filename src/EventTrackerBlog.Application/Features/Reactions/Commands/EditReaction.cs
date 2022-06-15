using AutoMapper;
using EventTrackerBlog.Data.Common;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Reactions.Commands
{
    public class EditReaction : IRequest<ReactionResponseModel>
    {
        public Guid ReactionId { get; }
        public Guid CommentId { get; }
        public ReactionType Type { get; set; }

        public EditReaction(Guid reactionId, Guid commentId, ReactionType type)
        {
            ReactionId = reactionId;
            CommentId = commentId;
            Type = type;
        }

        public class EditReactionHandler : IRequestHandler<EditReaction, ReactionResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public EditReactionHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context; 
                _mapper = mapper;
            }

            public async Task<ReactionResponseModel> Handle(EditReaction request, CancellationToken cancellationToken)
            {
                var reactionToEdit = _context.Reactions.FirstOrDefault(r => r.Id == request.ReactionId);

                if (reactionToEdit == null)
                {
                    throw new ItemDoesNotExistException();
                }

                reactionToEdit.Type = request.Type;
                reactionToEdit.CommentId = request.CommentId;

                await _context.SaveChangesAsync(cancellationToken); 

                return _mapper.Map<ReactionResponseModel>(reactionToEdit);
            }
        }
    }
}