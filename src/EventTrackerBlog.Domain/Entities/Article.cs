using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Domain.Common;
using BaseEntity = EventTrackerBlog.Domain.Entities.BaseEntity;
using Comment = EventTrackerBlog.Domain.Entities.Comment;

namespace EventTrackerBlog.Domain.Entities
{
    using static DataConstants;

    public class Article : BaseEntity
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}