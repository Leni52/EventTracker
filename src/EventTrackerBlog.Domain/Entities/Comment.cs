using System;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Data.Common;

namespace EventTrackerBlog.Data.Entities
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