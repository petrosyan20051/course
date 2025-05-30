using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;

using TypeId = int;

namespace db.Repositories {
    namespace db.Repositories {
        public class RateRepository : IRepository<Rate, TypeId> {
            private readonly OrderDbContext _context;

            public RateRepository(OrderDbContext context) {
                _context = context;
            }

            public async Task<Rate> GetByIdAsync(TypeId id) {
                return await _context.Rates.FirstOrDefaultAsync(o => o.Id == id);
            }

            public async Task<IEnumerable<Rate>> GetAllAsync() {
                return await _context.Rates.ToListAsync();
            }

            public async Task AddAsync(Rate entity) {
                var rate = await _context.Rates
                    .Where(o => o.Id == entity.Id)
                    .FirstOrDefaultAsync(o => o.Id == entity.Id);
                if (rate != null && rate.isDeleted is null) {
                    throw new InvalidDataException("New entity must have original id");
                }
                await _context.Rates.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Rate entity) {
                _context.Rates.Update(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(TypeId id) {
                var entity = await GetByIdAsync(id);
                if (entity != null) {
                    entity.isDeleted = DateTime.Now; // soft delete
                    entity.WhenChanged = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<TypeId> NewIdToAdd() {
                var entities = await GetAllAsync();
                if (entities == null)
                    return -1; // entities are not found

                // Get All deleted Ids in ascending order
                var deletedIds = entities
                    .Where(e => e.isDeleted != null)
                    .Select(e => e.Id)
                    .OrderBy(id => id)
                    .ToList();
                if (deletedIds.Any())
                    return deletedIds.First(); // return first free id
                var usedIds = new HashSet<TypeId>(entities.Select(c => c.Id).Where(id => id > 0).OrderBy(id => id));
                for (TypeId i = 1; i < TypeId.MaxValue; i++) {
                    if (!usedIds.Contains(i))
                        return i;
                }
                return TypeId.MaxValue; // maybe all seats are reserved
            }
        }
    }
}
