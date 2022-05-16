using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityProvider.Interfaces
{
    public interface IRolesService
    {
        Task<string> AddRole(string roleName);
        Task<bool> RemoveRole(string roleName);
        Task<List<IdentityRole>> GetAllRoles();
        Task<List<IdentityUser>> GetAllUsersInRole(string roleName);
        Task<bool> AddUserToRole(string roleName, string userId);
        Task<bool> RemoveUserFromRole(string roleName, string userId);
        Task<IdentityUser> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
        Task<bool> IsUserAdminOrEventHolder(IdentityUser user);
        Task<bool> IsRegularUser(IdentityUser user);
    }
}
