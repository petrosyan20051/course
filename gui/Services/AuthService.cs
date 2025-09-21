using System.Net.Http.Json;
using db.Models;
using DbAPI.DTO;
using Microsoft.Extensions.Configuration;

namespace gui.Services
{
    public class AuthService : BaseApiService<Credential>
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseUrl;

        public const string EntityPath = "Credential/";

        public AuthService()
        {
            // Use IP address from appconfig.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

            BaseUrl = config.GetSection("ServerConfig:ServerIP").Value;
            if (string.IsNullOrEmpty(BaseUrl))
            {
                throw new Exception("ServerIP не найден в конфигурации");
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            // Enforce http client send JSON format response
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            // Create prompt
            var loginPrompt = new LoginPrompt
            {
                Login = username,
                Password = password
            };

            // Try get access to api/Login
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Login", loginPrompt);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterPrompt registerPrompt)
        {
            // Try get access to api/Register
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Register", registerPrompt);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RegisterResponse>();
        }

        public override async Task<IEnumerable<Credential>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Credential>>(EntityPath + "GetAll");
        }

        public override async Task<Credential?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Credential>(EntityPath + "GetById" + $"?id={id}");
        }

        public override async Task AddAsync(Credential entity)
        {
            var response = await _httpClient.PostAsJsonAsync(EntityPath + "Add", entity);
            response.EnsureSuccessStatusCode();
        }

        public override async Task UpdateAsync(Credential entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"{EntityPath}UpdateById?id={entity.Id}", entity);
            response.EnsureSuccessStatusCode();
        }

        public override async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(EntityPath + "Delete" + $"?id={id}");
            response.EnsureSuccessStatusCode();
        }

        public override async Task<bool> RecoverAsync(int id)
        {
            var response = await _httpClient.GetAsync(EntityPath + "Recover" + $"?id={id}");
            return response.IsSuccessStatusCode;
        }

        public override async Task SoftDeleteAsync(int id)
        {
            var response = await _httpClient.GetAsync(EntityPath + "SoftDelete" + $"?id={id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
