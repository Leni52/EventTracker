using System;
using System.ComponentModel.DataAnnotations;

namespace EventTrackerBlog.DAL.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
