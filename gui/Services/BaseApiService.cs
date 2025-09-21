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
        public abstract Task<bool> RecoverAsync(TKey id);
    }
}
