namespace db.Contexts {
    public interface BaseInterface<T> where T : class {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // Some addictive methods
        Task<bool> ExistsAsync(int id);
    }
}
