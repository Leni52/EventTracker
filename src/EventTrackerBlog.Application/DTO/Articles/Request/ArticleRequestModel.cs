using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.Domain.DTO.Articles.Request
{
    public class ArticleRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
