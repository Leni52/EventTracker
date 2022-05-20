using System;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Domain.Common;

namespace EventTrackerBlog.Domain.DTO.Comments.Request
{
    using static DataConstants;
    public class CommentRequestModel
    {
        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Content { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
    }
}