using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Data.Common;
using BaseEntity = EventTrackerBlog.Data.Entities.BaseEntity;
using Comment = EventTrackerBlog.Data.Entities.Comment;

namespace EventTrackerBlog.Data.Entities
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