using db.Models;
using DbAPI.DTO;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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
            var loginResponse = await ExecuteApiCallAsync<LoginResponse>(() =>
                _httpClient.PostAsJsonAsync(EntityPath + "login", loginPrompt));

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
            return await ExecuteApiCallAsync<RegisterResponse>(() =>
                _httpClient.PostAsJsonAsync(EntityPath + "register", registerPrompt));
        }

        public override async Task<IEnumerable<Credential>?> GetAllAsync() {
            EnsureAuthorized();
            return await ExecuteApiCallAsync<IEnumerable<Credential>>(() =>
                _httpClient.GetAsync(EntityPath));
        }

        public override async Task<Credential?> GetByIdAsync(int id) {
            EnsureAuthorized();
            return await ExecuteApiCallAsync<Credential>(() =>
                _httpClient.GetAsync($"{EntityPath}{id}"));
        }

        public override async Task AddAsync(Credential entity) {
            EnsureAuthorized();
            await ExecuteApiCallAsync(() => _httpClient.PostAsJsonAsync(EntityPath, entity));
        }

        public override async Task UpdateAsync(Credential entity) {
            EnsureAuthorized();
            await ExecuteApiCallAsync(() => _httpClient.PutAsJsonAsync($"{EntityPath}{entity.Id}", entity));
        }

        public override async Task SoftDeleteAsync(int id) {
            EnsureAuthorized();
            await ExecuteApiCallAsync(() => _httpClient.DeleteAsync($"{EntityPath}{id}"));
        }

        public override async Task RecoverAsync(int id) {
            EnsureAuthorized();
            await ExecuteApiCallAsync(() =>
                _httpClient.PatchAsync($"{EntityPath}{id}/recover", null));
        }

        private void EnsureAuthorized() {
            if (!IsAuthenticated)
                throw new UnauthorizedAccessException("Требуется авторизация. Войти в систему");
        }
    }
}
