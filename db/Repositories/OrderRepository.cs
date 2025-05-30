using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;

using TypeId = int;

namespace db.Repositories {
    public class OrderRepository : IRepository<Order, TypeId> {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(TypeId id) {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync() {
            return await _context.Orders.Where(o => o.isDeleted == null)
                .ToListAsync();
        }

        public async Task AddAsync(Order entity) {
            var order = await _context.Orders
                .Where(o => o.Id == entity.Id)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (order != null && order.isDeleted is null) {
                throw new InvalidDataException("New entity must have original id");
            }
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity) {
            _context.Orders.Update(entity);
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
