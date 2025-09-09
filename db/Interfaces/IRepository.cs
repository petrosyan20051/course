namespace db.Interfaces {
    public interface IRepository<TEntity, TKey> : IRecovarable<TKey>, IDeletable<TKey> {

        // Async versions
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>?> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        // Some addictive async methods
        Task<TKey> NewIdToAddAsync();
    }
}
