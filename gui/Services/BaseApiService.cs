using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
using TKey = int;

namespace gui.Services {
    public abstract class BaseApiService<TEntity> where TEntity : class {
        protected readonly HttpClient _httpClient;

        public abstract Task<TEntity?> GetByIdAsync(TKey id);
        public abstract Task<IEnumerable<TEntity>?> GetAllAsync();
        public abstract Task AddAsync(TEntity entity);
        public abstract Task UpdateAsync(TEntity entity);
        //public abstract Task DeleteAsync(TKey id);
        public abstract Task SoftDeleteAsync(TKey id);
        public abstract Task RecoverAsync(TKey id);

        protected async Task<TResult> ExecuteApiCallAsync<TResult>(Func<Task<HttpResponseMessage>> apiCall) {
            try {
                var response = await apiCall();
                return await HandleResponseAsync<TResult>(response);
            } catch (HttpRequestException ex) {
                throw ex;
            } catch (JsonException ex) {
                throw ex;
            }
        }

        protected async Task ExecuteApiCallAsync(Func<Task<HttpResponseMessage>> apiCall) {
            try {
                var response = await apiCall();
                await HandleResponseAsync<object>(response);
            } catch (HttpRequestException ex) {
                throw;
            } catch (JsonException ex) {
                throw;
            }
        }

        protected async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response) {
            
            if (response.IsSuccessStatusCode) {
                if (response.StatusCode == HttpStatusCode.OK) {
                    return await response.Content.ReadFromJsonAsync<T>();
                } else if (response.StatusCode == HttpStatusCode.NoContent) {
                    return default;
                }
            }

            var content = await response.Content.ReadAsStringAsync();

            try {
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content);
                throw new HttpRequestException(errorResponse?.Message ?? $"Ошибка: {response.StatusCode}");
            } catch (JsonException) {
                throw new JsonException($"Ошибка: {content}");
            }
        }
    }
}
