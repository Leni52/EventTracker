using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Entities
{
    public class ExternalUser : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        public Guid ExternalUserId { get; set; }
    }
}
