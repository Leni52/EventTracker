using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.CommentModels
{
    public class CommentViewModel
    {
        public Guid EventId { get; set; }
        public string Text { get; set; }
    }
}
