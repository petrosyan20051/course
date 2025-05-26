using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class Rate : BaseModel {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        public int MovePrice { set; get; }
        public int IdlePrice { set; get; }

        //public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}