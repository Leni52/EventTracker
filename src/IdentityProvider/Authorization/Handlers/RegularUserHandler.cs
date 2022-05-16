using IdentityProvider.Authorization.Requirements;
using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace IdentityProvider.Authorization.Handlers
{
    public class RegularUserHandler : AuthorizationHandler<RegularUserRequirement>
    {
        private readonly IRolesService _rolesService;

        public RegularUserHandler(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, RegularUserRequirement requirement)
        {
            var currentUser = await _rolesService.GetCurrentUser(context.User);
            bool isRegularUser = await _rolesService.IsRegularUser(currentUser);

            if (isRegularUser)
            {
                context.Succeed(requirement);
            }

            context.Fail();
        }
    }
}