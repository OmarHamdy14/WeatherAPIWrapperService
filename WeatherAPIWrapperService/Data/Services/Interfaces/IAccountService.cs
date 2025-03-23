using Microsoft.AspNetCore.Identity;
using WeatherAPIWrapperService.Data.DTOs;

namespace WeatherAPIWrapperService.Data.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityUser> GetUserById(string id);
        Task<IdentityUser> GetUserByEmail(string email);
        Task<IdentityResult> Register(RegisterUserDTO model);
        Task<IdentityResult> AddUserToRole(string role, IdentityUser user);
    }
}
