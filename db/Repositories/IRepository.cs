namespace db.Repositories {
    public interface IRepository<TEntity, TKey> where TEntity : class {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);

        // Some addictive methods
        Task<TKey> NewIdToAdd();
    }
}
