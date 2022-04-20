using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Entities
{
    public class ExternalUser : BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public Guid ExternalUserId { get; set; }
    }
}
