using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class Rate : BaseModel {
        public string Forename { get; set; } = string.Empty;

        [ForeignKey("Driver")]
        public TypeId DriverId { get; set; }

        [ForeignKey("Vehicle")]
        public TypeId VehicleId { get; set; }

        public int MovePrice { set; get; }
        public int IdlePrice { set; get; }

        //public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}