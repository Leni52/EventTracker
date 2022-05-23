using EventTracker.DAL.Entities;
using System.Threading.Tasks;

namespace EventTracker.DAL.Contracts
{
    public interface IExternalUserRepository : IGenericRepository<ExternalUser>
    {
        public Task<ExternalUser> GetByExternalId(string externalId);
    }
}
