using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherAPIWrapperService.Data.DTOs;

namespace WeatherAPIWrapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.Name,
                };
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(res.Errors);
            }
            return BadRequest(ModelState); 
        }
    }
}
