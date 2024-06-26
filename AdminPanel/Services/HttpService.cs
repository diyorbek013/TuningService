using AdminPanel.StaticData;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Delivery.Services
{
    public class HttpService
    {

        private readonly CookieService _cookieService;
        string url;
        public HttpService(CookieService cookieService, IConfiguration configure)
        {
            _cookieService = cookieService;
            url = ConfigurationBinder.Get<string>(configure.GetSection("url")) ?? "";
        }

        private async Task SetAuthorizationHeader(HttpClient httpClient)
        {

            var token = await _cookieService.GetCookie(Keys.token);
            //var tokenData = JsonConvert.DeserializeObject<string>(token ?? string.Empty) ?? new();
            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task<HttpClient> GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(url),
                Timeout = TimeSpan.FromSeconds(30)
            };
            await SetAuthorizationHeader(client);
            return client;
        }

        private async Task<HttpResponseMessage> SendRequest(Func<HttpClient, Task<HttpResponseMessage>> requestFunc)
        {
            try
            {
                using (var client = await GetAuthenticatedClient())
                {
                    return await requestFunc(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in HttpService: {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public async Task<HttpResponseMessage> Get(string url) =>
            await SendRequest(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync(url);
            });

        public async Task<HttpResponseMessage> Post<T>(string url, T model) =>
            await SendRequest(client =>
            {
                var param = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                return client.PostAsync(url, param);
            });

        public async Task<HttpResponseMessage> PostWithoutBody(string url) =>
            await SendRequest(client => client.PostAsync(url, null));

        public async Task<HttpResponseMessage> Delete(string url) =>
            await SendRequest(client => client.DeleteAsync(url));

        public async Task<HttpResponseMessage> Put<T>(string url, T model) =>
            await SendRequest(client =>
            {
                var param = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                return client.PutAsync(url, param);
            });
    }
}
