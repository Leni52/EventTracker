using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityProvider.Interfaces
{
    public interface ITokenService
    {
        Task<IdentityUser> Register(string userName, string password);
        Task<TokenModel> Login(string userName, string password);
        Task<TokenModel> RefreshToken(TokenModel token);
        Task<TokenModel> CreateAccessToken(IdentityUser user);
    }
}
