using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.CommentModels
{
    public class CommentCreateModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
