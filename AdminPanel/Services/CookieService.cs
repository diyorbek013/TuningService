using Microsoft.JSInterop;

namespace CRM_Delivery.Services
{
    public class CookieService
    {
        private readonly IJSRuntime _jsRuntime;
        

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetCookie(string name)
        {
            return await _jsRuntime.InvokeAsync<string>("getCookie", name);
        }

        public async Task SetCookie(string name, string value, int minutes)
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", name, value, minutes);
        }
        public async Task SetCookieWithExpiryDateTime(string name, string value, DateTime expirationDateTime)
        {
            await _jsRuntime.InvokeVoidAsync("setCookieWithExpirationDateTime", name, value, expirationDateTime);
        }

        public async Task DeleteCookie(string name)
        {
            await _jsRuntime.InvokeVoidAsync("deleteCookie", name);
        }
    }
}
