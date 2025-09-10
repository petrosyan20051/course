using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    public class TransportVehicleRepository : IRepository<TransportVehicle, TypeId> {
        private readonly OrderDbContext _context;

        public TransportVehicleRepository(OrderDbContext context) {
            _context = context;
        }

        // Async versions
        public async Task<IEnumerable<TransportVehicle>?> GetAllAsync() {
            return await _context.TransportVehicles.ToListAsync();
        }

        public async Task<TransportVehicle?> GetByIdAsync(TypeId id) {
            return await _context.TransportVehicles.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(TransportVehicle entity) {
            var transportVehicle = await _context.TransportVehicles
                .Where(o => o.Id == entity.Id)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (transportVehicle != null) {
                throw new InvalidDataException("New entity must have original id");
            }
            await _context.TransportVehicles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(TypeId driverId, string number, string series, 
            int registrationCode, string model, string color, int releaseYear, string whoAdded,
            DateTime whenAdded, string? whoChanged = null, DateTime? whenChanged = null, string? note = null,
            DateTime? isDeleted = null) {

            if (number.IsNullOrEmpty()) {
                throw new ArgumentNullException("Number must be no empty string");
            } else if (series.IsNullOrEmpty()) {
                throw new ArgumentNullException("Series must be no empty string");
            } else if (model.IsNullOrEmpty()) {
                throw new ArgumentNullException("Model must be no empty string");
            } else if (color.IsNullOrEmpty()) {
                throw new ArgumentNullException("Color must be no empty string");
            } else if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!TransportVehicle.NumberValidate(number)) {
                throw new InvalidDataException("Number is invalid");
            } else if (!TransportVehicle.SeriesValidate(series)) {
                throw new InvalidDataException("Series is invalid");
            } else if (!TransportVehicle.RegistrationCodeValidate(registrationCode)) {
                throw new InvalidDataException("Registration code is invalid");
            } else if (!TransportVehicle.ReleaseYearValidate(releaseYear)) {
                throw new InvalidDataException("Release year is invalid");
            }

                TypeId id = await NewIdToAddAsync();
            if (id == -1)
                throw new DbUpdateException("Database has no available id for new entity");
            var entity = new TransportVehicle {
                DriverId = driverId,
                Number = number,
                Series = series,
                RegistrationCode = registrationCode,
                Model = model,
                Color = color,
                ReleaseYear = releaseYear,
                WhoAdded = whoAdded,
                WhenAdded = whenAdded,
                WhoChanged = whoChanged,
                WhenChanged = whenChanged,
                Note = note,
                isDeleted = isDeleted
            };

            await AddAsync(entity);
        }

        public async Task UpdateAsync(TransportVehicle entity) {
            _context.TransportVehicles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _context.TransportVehicles.Remove(entity);
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

        public async Task SoftDeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = DateTime.Now; // soft delete
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
