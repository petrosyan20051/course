namespace db.Interfaces {
    public interface IDeletable<TKey> {
        Task DeleteAsync(TKey id);
        Task SoftDeleteAsync(TKey id);
    }
}
