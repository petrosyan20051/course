using db.Models;
using DbAPI.DTO;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net.Http.Headers;

namespace gui.Services {
    public class AuthService : BaseApiService<Credential> {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;
        private string _token;

        public bool IsAuthenticated { get; private set; }

        public const string EntityPath = "Credential/";

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
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse> LoginAsync(string username, string password) {
            // Create prompt
            var loginPrompt = new LoginPrompt {
                Login = username,
                Password = password
            };

            // Get response
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Login", loginPrompt);
            var loginResponse = await HandleResponseAsync<LoginResponse>(response);
            
            _token = loginResponse.Token; // set current session token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            IsAuthenticated = true;

            return loginResponse;
        }

        public void LogOut() {
            _token = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            IsAuthenticated = false;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterPrompt registerPrompt) {
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Register", registerPrompt);
            return await HandleResponseAsync<RegisterResponse>(response);
        }

        public override async Task<IEnumerable<Credential>?> GetAllAsync() {
            EnsureAuthorized();
            return await _httpClient.GetFromJsonAsync<IEnumerable<Credential>>(EntityPath + "GetAll");
        }

        public override async Task<Credential?> GetByIdAsync(int id) {
            EnsureAuthorized();
            return await _httpClient.GetFromJsonAsync<Credential>(EntityPath + "GetById" + $"?id={id}");
        }

        public override async Task AddAsync(Credential entity) {
            EnsureAuthorized();
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Add", entity);
            response.EnsureSuccessStatusCode();
        }

        public override async Task UpdateAsync(Credential entity) {
            EnsureAuthorized();
            var response = await _httpClient.PutAsJsonAsync($"{EntityPath}UpdateById?id={entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public override async Task DeleteAsync(int id) {
            EnsureAuthorized();
            var response = await _httpClient.DeleteAsync(EntityPath + "Delete" + $"?id={id}");
            response.EnsureSuccessStatusCode();
        }

        public override async Task<bool> RecoverAsync(int id) {
            EnsureAuthorized();
            var response = await _httpClient.GetAsync(EntityPath + "Recover" + $"?id={id}");
            return response.IsSuccessStatusCode;
        }

        public override async Task SoftDeleteAsync(int id) {
            EnsureAuthorized();
            var response = await _httpClient.GetAsync(EntityPath + "SoftDelete" + $"?id={id}");
            response.EnsureSuccessStatusCode();
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response) {
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<T>();
            }

            var content = await response.Content.ReadAsStringAsync();

            try {
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content);
                throw new Exception(errorResponse.Message ?? $"Ошибка: {response.StatusCode}");
            } catch (JsonException) {
                throw new Exception($"Ошибка: {content}");
            }
        }

        private void EnsureAuthorized() {
            if (!IsAuthenticated)
                throw new UnauthorizedAccessException("Требуется авторизация. Войти в систему");
        }
    }
}
