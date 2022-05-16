using IdentityProvider.Authorization.Requirements;
using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace IdentityProvider.Authorization.Handlers
{
    public class AdminOrEventHolderHandler : AuthorizationHandler<AdminOrEventHolderRequirement>
    {
        private readonly IRolesService _rolesService;

        public AdminOrEventHolderHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrEventHolderRequirement requirement)
        {
            var currentUser = await _rolesService.GetCurrentUser(context.User);
            bool isAdminOrEventHolder = await _rolesService.IsUserAdminOrEventHolder(currentUser);

            if (isAdminOrEventHolder)
            {
                context.Succeed(requirement);
            }

            context.Fail();
        }
    }
}
