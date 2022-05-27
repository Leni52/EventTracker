using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Data.Common;

namespace EventTrackerBlog.Domain.DTO.Comments.Request
{
    using static DataConstants;
    public class CommentRequestModel
    {
        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Content { get; set; }
    }
}