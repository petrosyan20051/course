using db.Contexts;
using db.Interfaces;
using db.Models;
using db.Classes;
using Microsoft.EntityFrameworkCore;
using static db.Interfaces.IInformation;
using TypeId = int;
using Microsoft.IdentityModel.Tokens;

namespace db.Repositories {
    public class CredentialRepository : IRepository<Credential, TypeId>,
        IDeletable<TypeId>, IRecovarable<TypeId> {
        private readonly OrderDbContext _context;

        public CredentialRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Credential?> GetByIdAsync(TypeId id) {
            return await _context.Credentials.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Credential>?> GetAllAsync() {
            return await _context.Credentials.ToListAsync();
        }

        public async Task AddAsync(Credential entity) {

            await EntityValidate(entity.Username, entity.Password, entity.Rights, entity.WhoAdded,
                entity.WhenAdded, entity.Id, entity.WhoChanged, entity.WhenChanged, entity.Note,
                entity.isDeleted);

            await _context.Credentials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(string username, string password, UserRights Rights, string whoAdded,
            DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null, 
            string? note = null, DateTime? isDeleted = null) {

            await EntityValidate(username, password, Rights, whoAdded, whenAdded, id, whoChanged, 
                whenChanged, note, isDeleted);

            var entity = new Credential {
                Username = username,
                Password = password,
                Rights = Rights,
                WhoAdded = whoAdded,
                WhenAdded = whenAdded,
                WhoChanged = whoChanged,
                WhenChanged = whenChanged,
                Note = note,
                isDeleted = isDeleted
            };

            await _context.Credentials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task EntityValidate(string username, string password, UserRights Rights, string whoAdded,
            DateTime whenAdded, TypeId? id = null, string? whoChanged = null, DateTime? whenChanged = null,
            string? note = null, DateTime? isDeleted = null) {

            if (username.IsNullOrEmpty()) {
                throw new ArgumentNullException("Username must be no empty string");
            } else if (password.IsNullOrEmpty()) {
                throw new ArgumentNullException("Password must be no empty string");
            } else if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!PasswordHasher.IsPasswordStrong(password))
                throw new ArgumentException("Password is not strong. See db.Classes.PasswordHasher.IsPasswordStrong");


            if (id != 0) {
                throw new InvalidDataException("Entity must contain zero ID. Auto generation of ID is used");
            } else if (id == null && await NewIdToAddAsync() == -1)
                throw new DbUpdateException("Database has no available id for new entity");
        }

        public async Task UpdateAsync(Credential entity) {
            _context.Credentials.Update(entity);
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
                _context.Credentials.Remove(entity);
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
            if (entity != null) {
                entity.isDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
