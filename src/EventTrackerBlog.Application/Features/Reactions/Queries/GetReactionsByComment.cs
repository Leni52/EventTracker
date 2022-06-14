using AutoMapper;
using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Domain.DTO.Reactions.Response;
using EventTrackerBlog.Domain.Entities;
using ExceptionHandling.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var reactions = await _context.Comments.Where(c => c.Id == request.CommentId).
                    SelectMany(c => c.Reactions).ToListAsync(cancellationToken: cancellationToken);


                return _mapper.Map<IEnumerable<ReactionResponseModel>>(reactions);
            }
        }
    }
}
