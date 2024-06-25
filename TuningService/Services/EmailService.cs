using RestSharp;
using System.Text;

namespace TuningService.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = _configuration["Mailgun:ApiKey"];
            var domain = _configuration["Mailgun:Domain"];
            var client = new RestClient($"https://api.mailgun.net/v3/{domain}/messages");

            var request = new RestRequest();
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{apiKey}"))}");
            request.AddParameter("from", $"Diyorbek <mailgun@{domain}>");
            request.AddParameter("to", toEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("text", message);
            request.Method = Method.Post;

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {

            }
            else
            {

            }
        }
    }
}
