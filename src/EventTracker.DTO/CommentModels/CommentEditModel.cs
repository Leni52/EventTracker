using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.CommentModels
{
    public class CommentEditModel
    {
        [Required]
        public Guid CommentId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
