using System.ComponentModel.DataAnnotations;

using TypeId = int;

namespace db.Models {

    public class Driver {
        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [Display(Order = 2)]
        public string Forename { get; set; } = string.Empty;
        [Display(Order = 3)]
        public string Surname { get; set; } = string.Empty;
        [Display(Order = 4)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Order = 5)]
        public string DriverLicenceSeries { get; set; } = string.Empty;
        [Display(Order = 6)]
        public string DriverLicenceNumber { get; set; } = string.Empty;

        [Display(Order = 7)]
        public string WhoAdded { get; set; } = string.Empty;
        [Display(Order = 8)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 9)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 10)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 11)]
        public string? Note { get; set; } = null;
        [Display(Order = 12)]
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<TransportVehicle> TransportVehicles { get; set; } = new List<TransportVehicle>();
        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}