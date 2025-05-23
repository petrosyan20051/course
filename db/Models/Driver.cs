namespace db.Models {

    public class Driver {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DriverLicenceSeries { get; set; } = string.Empty;
        public string DriverLicenceNumber { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        public ICollection<TransportVehicle> TransportVehicles { get; set; } = new List<TransportVehicle>();
        public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}