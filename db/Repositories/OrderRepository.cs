using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    public class OrderRepository : IRepository<Order, TypeId>,
        IDeletable<TypeId>, IRecovarable<TypeId> {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(TypeId id) {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>?> GetAllAsync() {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddAsync(Order entity) {

            await EntityValidate(entity.CustomerId, entity.RouteId, entity.RateId, entity.Distance,
                entity.WhoAdded, entity.WhenAdded, entity.Id, entity.WhoChanged, entity.WhenChanged,
                entity.Note, entity.isDeleted);

            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(TypeId customerId, TypeId routeId, TypeId rateId, int distance, 
            string whoAdded, DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null, 
            string? note = null, DateTime? isDeleted = null) {

            await EntityValidate(customerId, routeId, rateId, distance, whoAdded, whenAdded, id,
                whoChanged, whenChanged, note, isDeleted);

            var entity = new Order {
                CustomerId = customerId,
                RouteId = routeId,  
                RateId = rateId,
                Distance = distance,
                WhoAdded = whoAdded,
                WhenAdded = whenAdded,
                WhoChanged = whoChanged,
                WhenChanged = whenChanged,
                Note = note,
                isDeleted = isDeleted
            };

            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task EntityValidate(TypeId customerId, TypeId routeId, TypeId rateId, int distance,
            string whoAdded, DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null,
            string? note = null, DateTime? isDeleted = null) {

            if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!Order.DistanceValidate(distance))
                throw new ArgumentException("Distance must be positive integer");

            if (await _context.Customers.AnyAsync(c => c.Id == customerId) == false) {
                throw new InvalidDataException($"Customer with id = {customerId} does not exist");
            } else if (await _context.Routes.AnyAsync(r => r.Id == routeId) == false) {
                throw new InvalidDataException($"Route with id = {routeId} does not exist");
            } else if (await _context.Rates.AnyAsync(r => r.Id == rateId) == false) {
                throw new InvalidDataException($"Rate with id = {rateId} does not exist");
            }

            if (id != 0) {
                throw new InvalidDataException("Entity must contain zero ID. Auto generation of ID is used");
            } else if (id == null && await NewIdToAddAsync() == -1)
                throw new DbUpdateException("Database has no available id for new entity");
        }

        public async Task UpdateAsync(Order entity) {
            _context.Orders.Update(entity);
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
                //.Where(e => e.isDeleted != null || e.isDeleted is null)
                .Select(e => e.Id)
                .OrderBy(id => id)
                .ToList();
            if (Ids.Last() == TypeId.MaxValue) {
                return -1; // all seats are reserved
            }

            return Ids.Last() + 1; // maybe all seats are reserved
        }

        public async Task DeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _context.Orders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
