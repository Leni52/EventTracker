using System;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Domain.Common;

namespace EventTrackerBlog.Domain.Entities
{
    using static DataConstants;
    public class Comment : BaseEntity
    {
        [Required]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
    }
}