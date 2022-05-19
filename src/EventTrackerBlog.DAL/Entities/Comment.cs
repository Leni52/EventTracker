using System;
using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.DAL.Entities
{
    using static Common.DataConstants;
    public class Comment : BaseEntity
    {
        [Required]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
    }
}
