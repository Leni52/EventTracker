using EventTracker.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.DAL.Entities
{
    using static Common.DataConstants;

    public class Article : BaseEntity
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }
    }
}
