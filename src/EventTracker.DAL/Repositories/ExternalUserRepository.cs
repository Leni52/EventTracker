using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventTracker.DAL.Repositories
{
    public class ExternalUserRepository : GenericRepository<ExternalUser>, IExternalUserRepository
    {
        public ExternalUserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<ExternalUser> GetByExternalId(string externalId)
        {
            return await  _context.ExternalUsers.FirstOrDefaultAsync(u => u.ExternalUserId.Equals(externalId));
        }
    }
}
