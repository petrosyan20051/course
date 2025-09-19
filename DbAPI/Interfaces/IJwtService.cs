using db.Models;
using System.Security.Claims;

namespace db.Interfaces {
    public interface IJwtService {
        string GenerateToken(Credential credential, Role role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
