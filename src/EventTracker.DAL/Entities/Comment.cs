using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Entities
{
    public class Comment : BaseEntity
    {
        [MaxLength(150)]
        public string Text { get; set; }
        public virtual Event Event { get; set; }
        public Guid EventId { get; set; }
    }
}
