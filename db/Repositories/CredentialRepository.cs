using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;

using TypeId = int;

namespace db.Repositories {
    public class CredentialRepository : IRepository<Credential, TypeId> {
        private readonly OrderDbContext _context;

        public CredentialRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Credential> GetByIdAsync(TypeId id) {
            return await _context.Credentials.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Credential>> GetAllAsync() {
            return await _context.Credentials.ToListAsync();
        }

        public async Task AddAsync(Credential entity) {
            var credential = await _context.Credentials
                .Where(o => o.Id == entity.Id)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (credential != null) {
                throw new InvalidDataException("New entity must have original id");
            }
            await _context.Credentials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Credential entity) {
            _context.Credentials.Update(entity);
            await _context.SaveChangesAsync();
        }

        /*public async Task SoftDeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = DateTime.Now; // soft delete
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }*/

        public async Task DeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _context.Credentials.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TypeId> NewIdToAdd() {
            throw new NotImplementedException();
        }

        public async Task<bool> RecoverAsync(TypeId id) {
            throw new NotImplementedException();
        }
    }
}
