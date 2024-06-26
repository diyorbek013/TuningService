using AdminPanel.StaticData;
using CRM_Delivery.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdminPanel.Services.ApiServices.AuthServices
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly CookieService cookieService;

        public CustomAuthenticationStateProvider(CookieService cookieService)
        {
            this.cookieService = cookieService;
            CurrentUser = GetAnonymous();
        }

        private ClaimsPrincipal CurrentUser { get; set; }


        private ClaimsPrincipal GetUser(string userName, string id)
        {

            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes. Sid, id),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, userName)
                }, "Authentication type");
            return new ClaimsPrincipal(identity);
        }


        private ClaimsPrincipal GetAnonymous()
        {
            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Sid, "0"),
                    new Claim(ClaimTypes.Name, "Anonymous"),
                }, null);

            return new ClaimsPrincipal(identity);
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //var accessToken = await JsRuntime.InvokeAsync<string>("appSession.read", "loginData");
                var accessToken = await cookieService.GetCookie(Keys.token);
                if (!string.IsNullOrEmpty(accessToken))
                    CurrentUser = this.GetUser("admin", accessToken);
            }
            catch (Exception)
            {

            }
            return new AuthenticationState(CurrentUser);
        }

        public async Task<AuthenticationState> ChangeUser(string? username, string? id)
        {
            CurrentUser = GetUser(username ?? "admin", id ?? "1");
            await Task.Delay(1);
            //await _sessionStorageService.SetItemAsStringAsync("key", id);            
            var task = GetAuthenticationStateAsync();
            this.NotifyAuthenticationStateChanged(task);
            return new AuthenticationState(CurrentUser);
        }

        public Task<AuthenticationState> Logout()

        {
            CurrentUser = GetAnonymous();
            var task = GetAuthenticationStateAsync();
            this.NotifyAuthenticationStateChanged(task);
            return task;
        }
    }
}
