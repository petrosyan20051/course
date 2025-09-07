using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;

using TypeId = int;

namespace db.Repositories {

    public class DriverRepository : IRepository<Driver, TypeId> {
        private readonly OrderDbContext _context;

        public DriverRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Driver> GetByIdAsync(TypeId id) {
            return await _context.Drivers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Driver>> GetAllAsync() {
            return await _context.Drivers.ToListAsync();
        }

        public async Task AddAsync(Driver entity) {
            var driver = await _context.Drivers
                .Where(o => o.Id == entity.Id)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (driver != null && driver.isDeleted is null) {
                throw new InvalidDataException("New entity must have original id");
            }
            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Driver entity) {
            _context.Drivers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = DateTime.Now; // soft delete
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _context.Drivers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TypeId> NewIdToAddAsync() {
            var entities = await GetAllAsync();
            if (entities == null)
                return 0; // entities are not found so can use id = 0

            // Get All deleted Ids in ascending order
            var Ids = entities
                .Where(e => e.isDeleted != null || e.isDeleted is null)
                .Select(e => e.Id)
                .OrderBy(id => id)
                .ToList();
            if (Ids.Last() == TypeId.MaxValue) {
                return -1; // all seats are reserved
            }

            return Ids.Last() + 1; // maybe all seats are reserved
        }

        public async Task<bool> RecoverAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity?.Id != null) {
                entity.isDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
