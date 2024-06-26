using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using CRM_Delivery.Services;
using AdminPanel.StaticData;

namespace AdminPanel.Services.ApiServices.AuthServices
{
    public class AuthenticatedRouteView : RouteView
    {
        [CascadingParameter]
        public AuthenticationService? AuthService { get; set; }
        private readonly NavigationManager NavigationManager;
        private readonly CookieService cookieService;


        public AuthenticatedRouteView(NavigationManager NavigationManager, CookieService cookieService)
        {
            this.NavigationManager = NavigationManager;
            this.cookieService = cookieService;
        }
        public bool isAuthorized;
        protected override async void Render(RenderTreeBuilder builder)
        {
            var tokenData = await cookieService.GetCookie(Keys.token);   
            isAuthorized = tokenData != null;
            if (!isAuthorized)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                base.Render(builder);
            }
        }
    }

}
