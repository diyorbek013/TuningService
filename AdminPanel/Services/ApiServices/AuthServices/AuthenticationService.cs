using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using CRM_Delivery.Services;
using AdminPanel.StaticData;
using Tuning.Library.Auth;
using Tuning.Library.Base;


namespace AdminPanel.Services.ApiServices.AuthServices
{
    public class AuthenticationService
    {
        // Inject HttpClient or any other service required for authentication        
        private readonly HttpService httpService;
        private readonly CookieService cookieService;
        private readonly NavigationManager navigation;

        public AuthenticationService(HttpService httpService, CookieService cookieService, NavigationManager manager)
        {
            this.cookieService = cookieService;
            this.httpService = httpService;
            navigation = manager;
        }

        public async Task<string> GetToken(LoginRequest? loginRequest)
        {
            try
            {
                var response = await httpService.Post("/api/Auth/login", loginRequest ?? new());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ApiBaseResultModel>(content) ?? new();
                    if (!data.Success)
                    {
                        var error = data.Message ?? "Неизвестная ошибка";
                        throw new Exception(data.Message);
                    }
                    string loginInfo = JsonConvert.SerializeObject(data.Data);
                    await cookieService.SetCookie(name: Keys.token, value: loginInfo, 115);
                    return (string)data.Data;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                else
                {
                    return "";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Logout()
        {
            // Implement logout logic here
            // Clear the authentication loginData and user information
        }
    }

}
