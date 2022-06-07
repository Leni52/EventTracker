using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;
using ExceptionHandling.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTrackerBlog.Application.Features.Reactions.Queries
{
    public class GetReactionById : IRequest<ReactionResponseModel>
    {
        public Guid ReactionId { get; }

        public GetReactionById(Guid reactionId)
        {
            ReactionId = reactionId;
        }

        public class GetReactionByIdHandler : IRequestHandler<GetReactionById, ReactionResponseModel>
        {
            private readonly BlogDbContext _context;
            private readonly IMapper _mapper;

            public GetReactionByIdHandler(BlogDbContext context, IMapper mapper)
            {
                _context = context; 
                _mapper = mapper;
            }

            public async Task<ReactionResponseModel> Handle(GetReactionById request, CancellationToken cancellationToken)
            {
                Reaction reactionToGet = _context.Reactions.FirstOrDefault(r => r.Id == request.ReactionId);

                if (reactionToGet == null)
                {
                    throw new ItemDoesNotExistException();
                }

                ReactionResponseModel result = _mapper.Map<ReactionResponseModel>(reactionToGet);

                return result;
            }
        }
    }
}