using Microsoft.AspNetCore.Authorization;

namespace IdentityProvider.Authorization.Requirements
{
    public class RegularUserRequirement : IAuthorizationRequirement
    {
        public RegularUserRequirement() { }
    }
}