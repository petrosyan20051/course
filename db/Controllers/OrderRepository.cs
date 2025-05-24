using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;

namespace db.Controllers {
    public class OrderRepository : IRepository<Order, int> {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id) {
            return await _context.Orders
                .Where(o => o.isDeleted == null)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync() {
            return await _context.Orders
                .Where(o => o.isDeleted == null)
                .ToListAsync();
        }   

        public async Task AddAsync(Order entity) {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity) {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = DateTime.Now; // soft delete
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        //public Task<bool> ExistsAsync(int id) {
        //    throw new NotImplementedException();
        //}
    }
}
