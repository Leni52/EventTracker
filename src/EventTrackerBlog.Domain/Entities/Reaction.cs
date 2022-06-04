using EventTrackerBlog.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.Domain.Entities
{
    public class Reaction : BaseEntity
    {
        [Required]
        public Guid CommentId { get; set; }
        [Required]
        public ReactionType Type { get; set; }
    }
}