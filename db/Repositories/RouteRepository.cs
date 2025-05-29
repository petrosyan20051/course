using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;

using TypeId = int;

namespace db.Repositories {
    namespace db.Repositories {
        public class RouteRepository : IRepository<Models.Route, TypeId> {
            private readonly OrderDbContext _context;

            public RouteRepository(OrderDbContext context) {
                _context = context;
            }

            public async Task<Models.Route> GetByIdAsync(TypeId id) {
                return await _context.Routes.FirstOrDefaultAsync(o => o.Id == id);
            }

            public async Task<IEnumerable<Models.Route>> GetAllAsync() {
                return await _context.Routes.ToListAsync();
            }

            public async Task AddAsync(Models.Route entity) {
                var route = await _context.Routes
                    .Where(o => o.Id == entity.Id)
                    .FirstOrDefaultAsync(o => o.Id == entity.Id);
                if (route != null && route.isDeleted is null) {
                    throw new InvalidDataException("New entity must have original id");
                }
                await _context.Routes.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Models.Route entity) {
                _context.Routes.Update(entity);
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
