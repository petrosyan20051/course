using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class TransportVehicle {

        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [ForeignKey("Driver")]
        [Display(Order = 1)]
        public TypeId DriverId { get; set; }

        [Display(Order = 2)]
        public string Number { get; set; } = string.Empty;
        [Display(Order = 3)]
        public string Series { get; set; } = string.Empty;
        [Display(Order = 4)]
        public int RegistrationCode { get; set; }
        [Display(Order = 5)]
        public string Model { get; set; } = string.Empty;
        [Display(Order = 6)]
        public string Color { get; set; } = string.Empty;
        [Display(Order = 7)]
        public string ReleaseYear { get; set; } = string.Empty;

        [Display(Order = 8)]
        public string WhoAdded { get; set; } = string.Empty;
        [Display(Order = 9)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 10)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 11)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 12)]
        public string? Note { get; set; } = null;
        [Display(Order = 13)]
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}