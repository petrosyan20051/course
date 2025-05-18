namespace db.Models {

    public class Driver {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DriverLicenceSeries { get; set; } = string.Empty;
        public string DriverLicenceNmber { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string WhoChanged { get; set; } = string.Empty;
        public DateTime WhenChanged { get; set; }
        public string Note { get; set; } = string.Empty;

        public ICollection<TransportVehicle> TransportVehicles { get; set; } = new List<TransportVehicle>();
        public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}