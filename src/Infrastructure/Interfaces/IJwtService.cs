using src.Core.Entities;
using System.Security.Claims;

namespace src.Infrastructure.Interfaces {
    public interface IJwtService {
        string GenerateToken(Credential credential, Role role);
        ClaimsPrincipal ValidateToken(string token);
        int GetTokenLifeTime();
    }
}
