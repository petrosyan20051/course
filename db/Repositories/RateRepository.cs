using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

            //public Task<bool> ExistsAsync(int id) {
            //    throw new NotImplementedException();
            //}
        }
    }
}
