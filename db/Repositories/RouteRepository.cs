using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    namespace db.Repositories {
        public class RouteRepository : IRepository<Models.Route, TypeId> {
            private readonly OrderDbContext _context;

            public RouteRepository(OrderDbContext context) {
                _context = context;
            }

            public async Task<Models.Route?> GetByIdAsync(TypeId id) {
                return await _context.Routes.FirstOrDefaultAsync(o => o.Id == id);
            }

            public async Task<IEnumerable<Models.Route>?> GetAllAsync() {
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

            public async Task AddAsync(string boardingAddress, string dropAddress, string whoAdded,
            DateTime whenAdded, string? whoChanged = null, DateTime? whenChanged = null, string? note = null,
            DateTime? isDeleted = null) {

                if (boardingAddress.IsNullOrEmpty()) {
                    throw new ArgumentNullException("Boarding address must be no empty string");
                } else if (dropAddress.IsNullOrEmpty()) {
                    throw new ArgumentNullException("Drop address must be no empty string");
                } else if (whoAdded.IsNullOrEmpty()) {
                    throw new ArgumentNullException("\"Who added\" must be no empty string");
                }

                TypeId id = await NewIdToAddAsync();
                if (id == -1)
                    throw new DbUpdateException("Database has no available id for new entity");
                var entity = new Models.Route {
                    Id = id,
                    BoardingAddress = boardingAddress,
                    DropAddress = dropAddress,
                    WhoAdded = whoAdded,
                    WhenAdded = whenAdded,
                    WhoChanged = whoChanged,
                    WhenChanged = whenChanged,
                    Note = note,
                    isDeleted = isDeleted
                };

                await AddAsync(entity);
            }

            public async Task UpdateAsync(Models.Route entity) {
                _context.Routes.Update(entity);
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
                    _context.Routes.Remove(entity);
                    await _context.SaveChangesAsync();
                }
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
        }
    }
}
