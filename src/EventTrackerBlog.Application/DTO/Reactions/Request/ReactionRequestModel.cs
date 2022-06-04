using EventTrackerBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTrackerBlog.Domain.DTO.Reactions.Request
{
    public class ReactionRequestModel
    {
        [Required]
        public Guid CommentId { get; set; }

        [Required]
        public ReactionType Type { get; set; }
    }
}
