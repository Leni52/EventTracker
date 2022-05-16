using Microsoft.AspNetCore.Authorization;

namespace IdentityProvider.Authorization.Requirements
{
    public class AdminOrEventHolderRequirement : IAuthorizationRequirement
    {
        public AdminOrEventHolderRequirement() { }
    }
}