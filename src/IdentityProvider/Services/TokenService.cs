using IdentityProvider.Exceptions;
using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IdentityProvider.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JWTConfig _options;
        private readonly TimeSpan ExpiryDuration = new TimeSpan(0, 60, 0);

        public TokenService(UserManager<IdentityUser> userManager, IOptions<JWTConfig> options)
        {
            _userManager = userManager;
            _options = options.Value;
        }  
<<<<<<< HEAD

        public async Task<TokenModel> Login(string userName, string password)
        {
            IdentityUser loggingUser = await _userManager.FindByNameAsync(userName)
                ?? throw new NonExistingUserException("User does not exist");

            bool credentialsAreValid = await _userManager.CheckPasswordAsync(loggingUser, password); 

            if (!credentialsAreValid)
                throw new InvalidCredentialsException("Wrong username or password");

            return await BuildToken(loggingUser);
=======
        
        public async Task<string> Login(string userName, string password)
        {                
            return BuildToken(userName);
>>>>>>> 8d9935f2c11c9fca244ec29ec84b5e7edadd26eb
        }

        public async Task<IdentityUser> Register(string userName, string password)
        {
            const string role = "RegularUser";

<<<<<<< HEAD
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IdentityUser userToBeCreated = new IdentityUser();
                //check if username is unique
                userToBeCreated.UserName = userName;
=======
     await _userManager.CreateAsync(userToBeCreated, password);
>>>>>>> 8d9935f2c11c9fca244ec29ec84b5e7edadd26eb

                await _userManager.CreateAsync(userToBeCreated, password);

                IdentityUser newlyCreatedUser = await _userManager.FindByNameAsync(userName);

                await _userManager.AddToRoleAsync(newlyCreatedUser, role);

                transactionScope.Complete();

                return newlyCreatedUser;
            }
        }

        public async Task<TokenModel> BuildToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("sub", user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //claims.AddRange(_options.Value.Audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Issuer, _options.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);

            var tokenModel = new TokenModel()
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                Expires_in = ExpiryDuration.TotalSeconds.ToString()
            };

            //This will be fixed

            return tokenModel;
        }

    }
}
