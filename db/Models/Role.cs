using System.ComponentModel.DataAnnotations;
using static db.Interfaces.IInformation;
using TypeId = int;

namespace db.Models {
    public class Role {
        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [Display(Order = 2)]
        public required string Forename { get; set; }

        [Display(Order = 3)]
        public UserRights Rights { get; set; }

        [Display(Order = 4)]
        public bool Get { get; set; }
        [Display(Order = 5)]
        public bool Post { get; set; }
        [Display(Order = 6)]
        public bool Update { get; set; }
        [Display(Order = 7)]
        public bool Delete { get; set; }

        [Display(Order = 8)]
        public required string WhoAdded { get; set; }
        [Display(Order = 9)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 10)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 11)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 12)]
        public DateTime? isDeleted { get; set; } = null;
    }
}
