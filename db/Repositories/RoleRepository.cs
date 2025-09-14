using db.Contexts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using static db.Interfaces.IInformation;

using TypeId = int;

namespace db.Repositories {
    public class RoleRepository {

        private readonly OrderDbContext _context;

        public RoleRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(TypeId id) {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
        }
      
        public async Task<Role?> GetByUserRights(UserRights rights) {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Rights == rights);
        }
    }
}
