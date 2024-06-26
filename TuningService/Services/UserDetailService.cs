using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TuningService.Data;

namespace TuningService.Services
{
    public class UserDetailService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDetailService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //public async Task<List<UserModel>> GetAllUsersAsync()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    return users.Select(user => new UserModel
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        Name = user.UserName
        //    }).ToList();
        //}

        //public async Task<UserDetailsModel> GetUserByIdAsync(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null) return null;

        //    // Assuming you have methods to get related data
        //    var cars = await GetCarsByUserIdAsync(userId);
        //    var tuningDetails = await GetTuningDetailsByUserIdAsync(userId);

        //    return new UserDetailsModel
        //    {
        //        Email = user.Email,
        //        Name = user.UserName,
        //        Cars = cars,
        //        TuningDetails = tuningDetails
        //    };
        //}

        //// Dummy methods for fetching related data
        //private Task<List<CarModel>> GetCarsByUserIdAsync(string userId)
        //{
        //    // Replace with actual data fetching logic
        //    return Task.FromResult(new List<CarModel>());
        //}

        //private Task<List<TuningDetailModel>> GetTuningDetailsByUserIdAsync(string userId)
        //{
        //    // Replace with actual data fetching logic
        //    return Task.FromResult(new List<TuningDetailModel>());
        //}
    }
}
