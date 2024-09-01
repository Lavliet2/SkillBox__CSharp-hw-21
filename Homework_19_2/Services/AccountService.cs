using WebAPIContacts.AuthContactApp;

namespace Homework_20.Services
{
    public class AccountService
    {
        public class LoginResult
        {
            public string Token { get; set; }
        }

        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public AccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["API:BaseUrl"];
        }

        public async Task<LoginResult> Login(UserLogin userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}/account/login", userLogin);            
            if (response.IsSuccessStatusCode && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                return await response.Content.ReadFromJsonAsync<LoginResult>();
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<bool> Register(UserRegistration userRegistration)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}/account/register", userRegistration);
            return response.IsSuccessStatusCode;
        }

    }
}
