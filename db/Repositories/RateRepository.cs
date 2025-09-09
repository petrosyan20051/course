using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    namespace db.Repositories {
        public class RateRepository : IRepository<Rate, TypeId> {
            private readonly OrderDbContext _context;

            public RateRepository(OrderDbContext context) {
                _context = context;
            }

            public async Task<Rate?> GetByIdAsync(TypeId id) {
                return await _context.Rates.FirstOrDefaultAsync(o => o.Id == id);
            }

            public async Task<IEnumerable<Rate>?> GetAllAsync() {
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

            public async Task AddAsync(string forename, TypeId driverId, TypeId vehicleId, int movePrice,
            int idlePrice, string model, string whoAdded, DateTime whenAdded, string? whoChanged = null, 
            DateTime? whenChanged = null, string? note = null, DateTime? isDeleted = null) {

                if (forename.IsNullOrEmpty()) {
                    throw new ArgumentNullException("Forename must be no empty string");
                } else if (model.IsNullOrEmpty()) {
                    throw new ArgumentNullException("Model must be no empty string");
                } else if (whoAdded.IsNullOrEmpty()) {
                    throw new ArgumentNullException("\"Who added\" must be no empty string");
                }

                if (movePrice <= 0)
                    throw new ArgumentException("Move price must be positive integer");
                else if (idlePrice <= 0)
                    throw new ArgumentException("Idle price must be positive integer");

                TypeId id = await NewIdToAddAsync();
                if (id == -1)
                    throw new DbUpdateException("Database has no available id for new entity");
                var entity = new Rate {
                    Id = id,
                    Forename = forename,
                    DriverId = driverId,    
                    VehicleId = vehicleId,
                    MovePrice = movePrice,
                    IdlePrice = idlePrice,
                    WhoAdded = whoAdded,
                    WhenAdded = whenAdded,
                    WhoChanged = whoChanged,
                    WhenChanged = whenChanged,
                    Note = note,
                    isDeleted = isDeleted
                };

                await AddAsync(entity);
            }

            public async Task UpdateAsync(Rate entity) {
                _context.Rates.Update(entity);
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
                    _context.Rates.Remove(entity);
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
