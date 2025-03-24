using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeatherAPIWrapperService.Data.DTOs;
using WeatherAPIWrapperService.Data.Services.Interfaces;

namespace WeatherAPIWrapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(IAccountService accountService, SignInManager<IdentityUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult res = await _accountService.Register(model);
                if (res.Succeeded)
                {
                    IdentityUser user = await _accountService.GetUserByEmail(model.Email);
                    Response.Cookies.Append("UserName", user.UserName); 
                    Response.Cookies.Append("UserId", user.Id);
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new {Message="Registration is succeeded"});
                }
                return BadRequest(res.Errors);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            if (ModelState.IsValid) 
            { 
                IdentityUser user = await _accountService.GetUserByEmail(model.Email);
                var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
                if (res.Succeeded)
                {
                    CookieOptions ops = new CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddHours(1)
                    };
                    Response.Cookies.Append("UserName", user.UserName, ops);
                    Response.Cookies.Append("UserId", user.Id, ops);
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new { Message = "Login is succeeded" });
                }
                return BadRequest(res);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("UserId");
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> AddAdmin(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var res = await _accountService.Register(model);
                if (res.Succeeded)
                {
                    IdentityUser user = await _accountService.GetUserByEmail(model.Email);
                    Response.Cookies.Append("UserName", user.UserName);
                    Response.Cookies.Append("UserId", user.Id);
                    var ress = await _accountService.AddUserToRole("Admin", user);
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new { Message = "Adding Admin is succeeded" });
                }
                return BadRequest(res.Errors);
            }
            return BadRequest(ModelState);
        }
    }
}
