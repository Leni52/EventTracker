using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.Requests
{
    public class CommentRequestDTO
    {
        [Required]
        public string CommenterId { get; set; }
        [Required]
        public Guid CommentId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
