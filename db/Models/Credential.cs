using db.Interfaces;
using System.ComponentModel.DataAnnotations;
using static db.Interfaces.IInformation;

using TypeId = int;

namespace db.Models {
    public class Credential {

        [Key]
        public TypeId Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public UserRights Rights { get; set; }

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;
    }
}
