using EventTracker.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Entities
{
    public class Event : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public Category Category { get; set; }
        [MaxLength(30)]
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<ExternalUser> Users { get; set; }
    }
}
