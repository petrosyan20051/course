using System.ComponentModel.DataAnnotations;
using static db.Interfaces.IInformation;

using TypeId = int;

namespace db.Models {
    public class Credential {

        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [Display(Order = 2)]
        public string Username { get; set; }
        [Display(Order = 3)]
        public string Password { get; set; }
        [Display(Order = 4)]
        public UserRights Rights { get; set; }

        [Display(Order = 5)]
        public string WhoAdded { get; set; } = string.Empty;
        [Display(Order = 6)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 7)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 8)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 9)]
        public string? Note { get; set; } = null;
        [Display(Order = 10)]
        public DateTime? isDeleted { get; set; } = null;
    }
}
