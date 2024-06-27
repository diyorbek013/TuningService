using AdminPanel;
using AdminPanel.Services;
using AdminPanel.Services.ApiServices.AuthServices;
using CRM_Delivery.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AdminPanel
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CookieService>();
            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<AuthenticatedRouteView>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<UserService>();

            await builder.Build().RunAsync();
        }
    }
}
