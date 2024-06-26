using Microsoft.AspNetCore.Identity;
using System.Net;
using TuningService.Data;
using TuningService.DTOs;

namespace TuningService.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            var user = new ApplicationUser
            {
                UserName = registrationDto.Email,
                Email = registrationDto.Email
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("user"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }
                await _userManager.AddToRoleAsync(user, "user");
                return user;
            }

            throw new Exception("User registration failed");
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
