using EventTrackerBlog.Domain.Common;
using System;

namespace EventTrackerBlog.Domain.DTO.Reactions.Response
{
    public class ReactionResponseModel
    {
        public Guid CommentId { get; set; }
        public Guid Id { get; set; }
        public ReactionType Type { get; set; }
    }
}
