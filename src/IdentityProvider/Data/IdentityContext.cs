using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Data
{
    public class IdentityContext : IdentityDbContext
    {
       public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }
    }
}
