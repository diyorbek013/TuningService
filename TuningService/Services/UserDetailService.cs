using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuning.Library.UserDetail;
using TuningService.Data;

namespace TuningService.Services
{
    public class UserDetailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        readonly ApplicationDbContext _context;

        public UserDetailService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return users.Select(user => new User
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            }).ToList();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new User
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };
        }

        public async Task<List<Tuning.Library.UserDetail.Car>> GetCarsByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var cars = await _context.Cars
                .Include(a => a.TuningDetails)
                .Where(a => a.UserId == userId)
                .Select(a => new Tuning.Library.UserDetail.Car()
                {
                    Id = a.Id,
                    BrandName = a.BrandName,
                    MadeYear = a.MadeYear,
                    Model = a.Model,
                    UserId = a.UserId,
                    TuningDetails = a.TuningDetails.Select(b => new Tuning.Library.UserDetail.TuningDetail()
                    {
                        CarId = b.CarId,
                        Description = b.Description,
                        Id = b.Id,
                        TuningPartOfCar = b.TuningPartOfCar
                    }).ToList()
                })
                .ToListAsync();

            return cars;
        }
    }
}
