using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {

    public class DriverRepository : IRepository<Driver, TypeId> {
        private readonly OrderDbContext _context;

        public DriverRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Driver?> GetByIdAsync(TypeId id) {
            return await _context.Drivers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Driver>?> GetAllAsync() {
            return await _context.Drivers.ToListAsync();
        }

        public async Task AddAsync(Driver entity) {

            await EntityValidate(entity.Forename, entity.Surname, entity.PhoneNumber, entity.DriverLicenceSeries,
                entity.DriverLicenceNumber, entity.WhoAdded, entity.WhenAdded, entity.Id, entity.WhoChanged,
                entity.WhenChanged, entity.Note, entity.IsDeleted);

            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(string forename, string surname, string phoneNumber,
            string driverLicenceSeries, string driverLicenceNumber, string whoAdded,
            DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null,
            string? note = null, DateTime? isDeleted = null) {

            await EntityValidate(forename, surname, phoneNumber, driverLicenceSeries, driverLicenceNumber,
                whoAdded, whenAdded, id, whoChanged, whenChanged, note, isDeleted);

            var entity = new Driver {
                Forename = forename,
                Surname = surname,
                PhoneNumber = phoneNumber,
                DriverLicenceSeries = driverLicenceSeries,
                DriverLicenceNumber = driverLicenceNumber,
                WhoAdded = whoAdded,
                WhenAdded = whenAdded,
                WhoChanged = whoChanged,
                WhenChanged = whenChanged,
                Note = note,
                IsDeleted = isDeleted
            };

            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task EntityValidate(string forename, string surname, string phoneNumber,
            string driverLicenceSeries, string driverLicenceNumber, string whoAdded,
            DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null,
            string? note = null, DateTime? isDeleted = null) {

            if (forename.IsNullOrEmpty()) {
                throw new ArgumentNullException("Forename must be no empty string");
            } else if (surname.IsNullOrEmpty()) {
                throw new ArgumentNullException("Surname must be no empty string");
            } else if (phoneNumber.IsNullOrEmpty()) {
                throw new ArgumentNullException("phone number must be no empty string");
            } else if (driverLicenceSeries.IsNullOrEmpty()) {
                throw new ArgumentNullException("Driver licence series must be no empty string");
            } else if (driverLicenceNumber.IsNullOrEmpty()) {
                throw new ArgumentNullException("Driver licence number year must be no empty string");
            } else if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!Driver.PhoneNumberValidate(phoneNumber)) {
                throw new ArgumentException("Invalid phone number");
            } else if (!Driver.DriverLicenceSeriesValidate(driverLicenceSeries)) {
                throw new ArgumentException("Invalid driver licence series");
            } else if (!Driver.DriverLicenceNumberValidate(driverLicenceNumber)) {
                throw new ArgumentException("Invalid driver licence number");
            }

            if (id != 0) {
                throw new InvalidDataException("Entity must contain zero ID. Auto generation of ID is used");
            } else if (id == null)
                throw new DbUpdateException("Database has no available id for new entity");
        }

        public async Task UpdateAsync(Driver entity) {
            _context.Drivers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.IsDeleted = DateTime.Now; // soft delete
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

        /*public async Task<TypeId> NewIdToAddAsync() {
            var entities = await GetAllAsync();
            if (entities == null)
                return 0; // entities are not found so can use id = 0

            // Get All deleted Ids in ascending order
            var Ids = entities
                .Where(e => e.IsDeleted != null || e.IsDeleted is null)
                .Select(e => e.Id)
                .OrderBy(id => id)
                .ToList();
            if (Ids.Last() == TypeId.MaxValue) {
                return -1; // all seats are reserved
            }

            return Ids.Last() + 1; // maybe all seats are reserved
        }*/

        public async Task<bool> RecoverAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity?.Id != null) {
                entity.IsDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
