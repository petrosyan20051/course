namespace DbAPI.DTO {
    public class RegisterResponse {
        public required string UserName { get; set; }

        public required bool CanGet { get; set; }
        public required bool CanPost { get; set; }
        public required bool CanUpdate { get; set; }
        public required bool CanDelete { get; set; }
    }
}
