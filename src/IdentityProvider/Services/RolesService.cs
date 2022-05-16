using IdentityProvider.Exceptions;
using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace IdentityProvider
{
    public class RolesService : IRolesService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> AddRole(string roleName)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IdentityRole roleToBeCreated = new IdentityRole();
                if (await _roleManager.RoleExistsAsync(roleName) == false && roleName.Length > 0)
                {
                    roleToBeCreated.Name = roleName;
                    await _roleManager.CreateAsync(roleToBeCreated);
                }

                transactionScope.Complete();
                return roleToBeCreated.Name;
            }
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                List<IdentityRole> roles = _roleManager.Roles.ToList();
                transactionScope.Complete();
                if (roles.Count > 0)
                    return roles;
                else throw new NoRolesException("There aren't any roles in the database.");
            }
        }

        public async Task<bool> RemoveRole(string roleName)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IdentityRole roleToBeDeleted = await _roleManager.FindByNameAsync(roleName);

                if (roleToBeDeleted == null)
                {
                    throw new RoleDoesNotExistException("Role doesn't exist.");
                }
                else
                  await _roleManager.DeleteAsync(roleToBeDeleted);
                transactionScope.Complete();
                return true;
            }
        }

        public async Task<List<IdentityUser>> GetAllUsersInRole(string roleName)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                List<IdentityUser> usersInRole = new List<IdentityUser>();
                List<IdentityUser> users = _userManager.Users.ToList();
                foreach (var u in users)
                {
                    if (await _userManager.IsInRoleAsync(u, roleName))
                    {
                        usersInRole.Add(u);
                    }
                }
                transactionScope.Complete();
                if (usersInRole.Count > 0)
                    return usersInRole;
                else
                    throw new NoUsersWithSuchRoleException("There aren't any users with that role.");
            }
        }

        public async Task<bool> AddUserToRole(string roleName, string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveUserFromRole(string roleName, string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<IdentityUser> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<bool> IsUserAdminOrEventHolder(IdentityUser user)
        {
            if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "EventHolder"))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsRegularUser(IdentityUser user)
        {
            if (await _userManager.IsInRoleAsync(user, "RegularUser"))
            {
                return true;
            }

            return false;
        }
    }
}
