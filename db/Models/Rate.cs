using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class Rate {
        
        [Key]
        public TypeId Id { get; set; }

        public string Forename { get; set; } = string.Empty;

        [ForeignKey("Driver")]
        public TypeId DriverId { get; set; }

        [ForeignKey("Vehicle")]
        public TypeId VehicleId { get; set; }

        public int MovePrice { set; get; }
        public int IdlePrice { set; get; }

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}