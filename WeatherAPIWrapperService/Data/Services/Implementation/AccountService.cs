using Microsoft.AspNetCore.Identity;
using WeatherAPIWrapperService.Data.DTOs;
using WeatherAPIWrapperService.Data.Services.Interfaces;
using WeatherAPIWrapperService.Models;

namespace WeatherAPIWrapperService.Data.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityUser> GetUserById(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);    
            return user;
        }
        public async Task<IdentityUser> GetUserByEmail(string email)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task<IdentityResult> Register(RegisterUserDTO model)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                PasswordHash = model.Password
            };
            var res = await _userManager.CreateAsync(user, model.Password);
            return res;
        }
        public async Task<IdentityResult> AddUserToRole(string role,IdentityUser user)
        {
            var res = await _userManager.AddToRoleAsync(user, role);
            return res;
        }
        public async Task CreateToken()
        {

        }
    }
}
