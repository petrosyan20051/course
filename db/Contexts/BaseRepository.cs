using Microsoft.EntityFrameworkCore;

using IdType = int;

namespace db.Contexts {
    public abstract class BaseRepository<T> : BaseInterface<T> where T : class {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(DbContext context) { 
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }


        public virtual async Task<T> GetByIdAsync(IdType id) {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(T entity) {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity) {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(IdType id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ExistsAsync(IdType id) {
            return await _dbSet.AnyAsync(e => EF.Property<IdType>(e, "Id") == id);
        }

        public virtual IQueryable<T> Query() {
            return _dbSet.AsQueryable();
        }
    }
}
