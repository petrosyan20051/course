﻿namespace db.Interfaces {
    public interface IRepository<TEntity, TKey> {

        // Async versions
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>?> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task SoftDeleteAsync(TKey id);
        Task<bool> RecoverAsync(TKey key);
    }
}
