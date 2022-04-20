using EventTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DAL.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
