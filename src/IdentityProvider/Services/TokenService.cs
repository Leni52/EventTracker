using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IOptions<JWTConfig> _options;
        private readonly TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);

        public TokenService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IOptions<JWTConfig> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options;
        }  

        public async Task<string> Login(string userName, string password)
        {
            SignInResult logInResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            //TODO: Error handling
            if (!logInResult.Succeeded)
                throw new Exception("Incorrect credentials");
                
            return BuildToken(userName);
        }

        public async Task<IdentityUser> Register(string userName, string password)
        {
            IdentityUser userToBeCreated = new IdentityUser();
            //check if username is unique
            userToBeCreated.UserName = userName;

            await _userManager.CreateAsync(userToBeCreated, password);

            return await _userManager.FindByNameAsync(userName);
        }
        public string BuildToken(string userName)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            };

            //claims.AddRange(_options.Value.Audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Value.Issuer, _options.Value.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
