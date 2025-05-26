namespace db.Models {

    public class Driver : BaseModel {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DriverLicenceSeries { get; set; } = string.Empty;
        public string DriverLicenceNumber { get; set; } = string.Empty;

        //public ICollection<TransportVehicle> TransportVehicles { get; set; } = new List<TransportVehicle>();
        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}