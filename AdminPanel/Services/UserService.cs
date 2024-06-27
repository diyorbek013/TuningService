using CRM_Delivery.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Tuning.Library.UserDetail;

namespace AdminPanel.Services
{
    public class UserService
    {
        private readonly HttpService _httpService;

        public UserService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpService.Get("/api/user");
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            return users;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var response = await _httpService.Get($"/api/user/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

        public async Task<List<Car>> GetUserCarsAsync(string userId)
        {
            var response = await _httpService.Get($"/api/user/{userId}/cars");
            var content = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<List<Car>>(content);
            return cars;
        }
    }
}
