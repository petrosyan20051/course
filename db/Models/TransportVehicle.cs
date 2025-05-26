using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class TransportVehicle : BaseModel {
        public int Id { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        public string Number { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public int RegistrationCode { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;

        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}