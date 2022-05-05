using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [Route("createrole")]
        [HttpPost]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            await _rolesService.AddRole(roleName);
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest();
            }
            return Ok(roleName);
        }
        [Route("deleterole")]
        [HttpDelete]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            await _rolesService.RemoveRole(roleName);
            return NoContent();
        }
        [Route("roles")]
        [HttpGet]
        public async Task<ActionResult> GetAllRoles()
        {
          var roles =  await _rolesService.GetAllRoles();
            return Ok(roles);
        }
        [HttpGet("usersinrole")]
        public async Task<ActionResult>GetAllUsersInRole(string roleName)
        {
            var users = await _rolesService.GetAllUsersInRole(roleName);
            return Ok(users);
        }
        [Route("addtorole")]
        [HttpPost]
        public async Task<ActionResult> AddUserToRole(string roleName, string userId)
        {
            if (await _rolesService.AddUserToRole(roleName, userId))
            {
                return Ok();
            }                
            else return BadRequest("User is already in role.");
        }
        [Route("removefromrole")]
        [HttpPost]
        public async Task<ActionResult> RemoveUserFromRole(string roleName, string userId)
        {
            if (await _rolesService.RemoveUserFromRole(roleName, userId))
            {
                return Ok();
            }               
            else return BadRequest("User is removed from the role.");
        }
    }
}
