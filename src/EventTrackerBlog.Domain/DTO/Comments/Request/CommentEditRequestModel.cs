using System;
using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.Domain.DTO.Comments.Request
{
    using static Common.DataConstants;
    public class CommentEditRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Content { get; set; }
    }
}