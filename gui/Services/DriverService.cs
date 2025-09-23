using db.Models;
using DbAPI.DTO;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace gui.Services {
    public class OrderService : BaseApiService<Order> {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;
        private string _token;

        public bool IsAuthenticated { get; private set; }

        public const string EntityPath = "Order/";

        public OrderService() {
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

        public void Login(string token) {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            IsAuthenticated = true;
        }

        public void LogOut() {
            _token = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            IsAuthenticated = false;
        }

        public override async Task<IEnumerable<Order>?> GetAllAsync() {
            EnsureAuthorized();
            return await ExecuteApiCallAsync<IEnumerable<Order>>(() =>
                _httpClient.GetAsync(EntityPath));
        }

        public override async Task<Order?> GetByIdAsync(int id) {
            EnsureAuthorized();
            return await ExecuteApiCallAsync<Order>(() =>
                _httpClient.GetAsync($"{EntityPath}{id}"));
        }

        public override async Task AddAsync(Order entity) {
            EnsureAuthorized();
            await ExecuteApiCallAsync(() => _httpClient.PostAsJsonAsync(EntityPath, entity));
        }

        public override async Task UpdateAsync(Order entity) {
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
