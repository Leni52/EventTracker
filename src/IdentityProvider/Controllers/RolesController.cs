using IdentityProvider.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

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
       
        [HttpPost]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            await _rolesService.AddRole(roleName);
            
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            await _rolesService.RemoveRole(roleName);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRoles()
        {
            var roles = await _rolesService.GetAllRoles();

            return Ok(roles);
        }

        [HttpGet]
        [Route("{roleName}/users")]
        public async Task<ActionResult> GetAllUsersInRole(string roleName)
        {
            var users = await _rolesService.GetAllUsersInRole(roleName);

            return Ok(users);
        }

        [Route("{roleName}/users/{userId}")]
        [HttpPost]
        public async Task<ActionResult> AddUserToRole(string roleName, string userId)
        {
            if (await _rolesService.AddUserToRole(roleName, userId))
            {
                return Ok();
            }

            else return Conflict("User is already in role.");
        }

        [Route("{roleName}/users/{userId}")]
        [HttpDelete]
        public async Task<ActionResult> RemoveUserFromRole(string roleName, string userId)
        {
            if (await _rolesService.RemoveUserFromRole(roleName, userId))
            {
                return Ok();
            }

            else return Conflict("User is removed from the role.");
        }
    }
}