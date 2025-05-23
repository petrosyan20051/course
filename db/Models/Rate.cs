using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class Rate {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public int MovePrice { set; get; }
        public int IdlePrice { set; get; }

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = string.Empty;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}