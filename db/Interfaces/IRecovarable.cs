namespace db.Interfaces {
    public interface IRecovarable<TKey> {
        Task<bool> RecoverAsync(TKey key);
    }
}
