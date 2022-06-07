using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventTrackerBlog.Data.Common;
using EventTrackerBlog.Domain.Entities;

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
        public List<Reaction> Reactions { get; set; }
    }
}