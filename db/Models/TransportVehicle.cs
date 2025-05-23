using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class TransportVehicle {
        public int Id { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public string Number { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public int RegistrationCode { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted {  get; set; } = null;

        public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}