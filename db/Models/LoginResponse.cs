using TypeId = int;

namespace db.Models {
    public class LoginResponse {
        public TypeId UserId { get; set; }
        public string Username { get; set; } = string.Empty;

        public bool CanGet { get; set; }
        public bool CanPost { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
