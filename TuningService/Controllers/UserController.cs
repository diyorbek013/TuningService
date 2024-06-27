using Microsoft.AspNetCore.Mvc;
using TuningService.Services;

namespace TuningService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserDetailService _userService;

        public UserController(UserDetailService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{userId}/cars")]
        public async Task<IActionResult> GetUserCars(string userId)
        {
            var cars = await _userService.GetCarsByUserIdAsync(userId);
            if (cars == null)
            {
                return NotFound();
            }
            return Ok(cars);
        }
    }
}
