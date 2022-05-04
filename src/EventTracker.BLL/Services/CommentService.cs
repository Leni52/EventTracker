using EventTracker.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class CommentService
    {
        private readonly DatabaseContext _context;

        public CommentService(DatabaseContext context)
        {
            _context = context;
        }
    }
}
