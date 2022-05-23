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
using System.Security.Cryptography;
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
        private readonly string sub = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public TokenService(UserManager<IdentityUser> userManager, IOptions<JWTConfig> options)
        {
            _userManager = userManager;
            _options = options.Value;
        }

        public async Task<TokenModel> Login(string userName, string password)
        {
            IdentityUser loggingUser = await _userManager.FindByNameAsync(userName)
                ?? throw new NonExistingUserException("User does not exist");

            bool credentialsAreValid = await _userManager.CheckPasswordAsync(loggingUser, password); 

            if (!credentialsAreValid)
                throw new InvalidCredentialsException("Wrong username or password");

            return await CreateAccessToken(loggingUser);

        }

        public async Task<IdentityUser> Register(string userName, string password)
        {
            const string role = "RegularUser";

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                IdentityUser userToBeCreated = new IdentityUser();
                //check if username is unique
                userToBeCreated.UserName = userName;

                await _userManager.CreateAsync(userToBeCreated, password);

                IdentityUser newlyCreatedUser = await _userManager.FindByNameAsync(userName);

                await _userManager.AddToRoleAsync(newlyCreatedUser, role);

                transactionScope.Complete();

                return newlyCreatedUser;
            }
        }
        public async Task<TokenModel> CreateAccessToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Issuer, _options.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);        

            var tokenModel = new TokenModel()
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                Expires_in = ExpiryDuration.TotalSeconds.ToString()

            };

            return CreateRefreshToken(tokenModel);
        }

        public async Task<TokenModel> CreateAccessToken(IdentityUser user, string jti)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("Role", role));
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Issuer, _options.Audience, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);

            var tokenModel = new TokenModel()
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor),
                Expires_in = ExpiryDuration.TotalSeconds.ToString()

            };

            return CreateRefreshToken(tokenModel);
        }

        public async Task<TokenModel> RefreshToken(TokenModel refreshToken)
        {
            ClaimsPrincipal accessTokenPrincipal = GetPrincipalFromExpiredToken(refreshToken.Access_token) ?? throw new InvalidTokenException("Invalid token");
            string tokenId = accessTokenPrincipal.Claims.Single(x => x.Type == "jti").Value;
            string userId = accessTokenPrincipal.Claims.Single(x => x.Type == sub).Value;

            IdentityUser user = await _userManager.FindByIdAsync(userId) ?? throw new NonExistingUserException("User does not exist");

            ClaimsPrincipal refreshTokenPrincipal = GetPrincipalFromExpiredToken(refreshToken.Refresh_token) ?? throw new InvalidTokenException("Invalid token");
            string refreshTokenAti = refreshTokenPrincipal.Claims.Single(x => x.Type == "ati").Value;

            if (tokenId != refreshTokenAti)
                throw new InvalidTokenException("Invalid token");


            var newToken = await CreateAccessToken(user, tokenId);

            newToken.Refresh_token = refreshToken.Refresh_token;
            newToken.Refresh_token_expiryTime = refreshToken.Refresh_token_expiryTime;

            return newToken;
        }

        private TokenModel CreateRefreshToken(TokenModel accessToken)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(accessToken.Access_token) ?? throw new InvalidTokenException("Invalid token");

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")),
                new Claim(JwtRegisteredClaimNames.Sub, principal.Claims.Single(x => x.Type == sub).Value),
                new Claim("ati", principal.Claims.Single(x => x.Type == "jti").Value)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenDescriptor = new JwtSecurityToken(_options.Issuer, _options.Audience, claims,
                expires: DateTime.Now.Add(TimeSpan.FromDays(7)), signingCredentials: credentials);

            accessToken.Refresh_token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            accessToken.Refresh_token_expiryTime = DateTime.Now.Add(TimeSpan.FromDays(7)).ToString("s");

            return accessToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
