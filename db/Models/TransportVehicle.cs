using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class TransportVehicle : BaseModel {
        [ForeignKey("Driver")]
        public TypeId DriverId { get; set; }

        public string Number { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public int RegistrationCode { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;

        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}