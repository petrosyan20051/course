using db.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace gui.Classes {
    public class AuthService {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;

        public AuthService() {
            // Use IP address from appconfig.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

            BaseUrl = config.GetSection("ServerConfig:ServerIP").Value;
            if (string.IsNullOrEmpty(BaseUrl)) {
                throw new Exception("ServerIP не найден в конфигурации");
            }

            _httpClient = new HttpClient {
                BaseAddress = new Uri(BaseUrl)
            };

            // Enforce http client send JSON format response
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse> LoginAsync(string username, string password) {
            // Create prompt
            var loginPrompt = new LoginPrompt {
                Login = username,
                Password = password
            };

            // Try get access to api/Login
            var response = await _httpClient.PostAsJsonAsync("Credential/Login", loginPrompt);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterPrompt registerPrompt) {
            // Try get access to api/Register
            var response = await _httpClient.PostAsJsonAsync("Register", registerPrompt);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RegisterResponse>();
        }
    }
}
