using db.Contexts;
using db.Interfaces;
using db.Models;
using db.Classes;
using Microsoft.EntityFrameworkCore;
using static db.Interfaces.IInformation;
using TypeId = int;
using Microsoft.IdentityModel.Tokens;

namespace db.Repositories {
    public class CredentialRepository : IRepository<Credential, TypeId> {
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
            var credential = await _context.Credentials
                .Where(o => o.Id == entity.Id)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);
            if (credential != null) {
                throw new InvalidDataException("New entity must have original id");
            }
            await _context.Credentials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(string username, string password, UserRights Rights, string whoAdded,
            DateTime whenAdded, string? whoChanged = null, DateTime? whenChanged = null, string? note = null,
            DateTime? isDeleted = null) {

            if (username.IsNullOrEmpty()) {
                throw new ArgumentNullException("Username must be no empty string");
            } else if (password.IsNullOrEmpty()) {
                throw new ArgumentNullException("Password must be no empty string");
            } else if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!PasswordHasher.IsPasswordStrong(password))
                throw new ArgumentException("Password is not strong. See db.Classes.PasswordHasher.IsPasswordStrong");


            TypeId id = await NewIdToAddAsync();
            if (id == -1)
                throw new DbUpdateException("Database has no available id for new entity");
            var entity = new Credential {
                Id = id,
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

            await AddAsync(entity);
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
            throw new NotImplementedException();
        }

        public async Task<bool> RecoverAsync(TypeId id) {
            throw new NotImplementedException();
        }
    }
}
