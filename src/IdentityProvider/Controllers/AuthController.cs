﻿using IdentityProvider.Interfaces;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            string token = await _tokenService.Login(username, password);

            TokenModel accessToken = new TokenModel { AccessToken = token };

            return Ok(accessToken);
        }

        [Route("register")]
        [HttpPost]
        public async Task Register(string username, string password)
        {
            await _tokenService.Register(username, password);
        }

    }
}
