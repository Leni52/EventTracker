using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityProvider.Interfaces
{
    public interface ITokenService
    {
        Task<IdentityUser> Register(string userName, string password);
        Task<string> Login(string userName, string password);
        string BuildToken(string userName);
    }
}
