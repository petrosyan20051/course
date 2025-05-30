using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class Order : BaseModel {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Route")]
        public int RouteId { get; set; }

        [ForeignKey("Rate")]
        public int RateId { get; set; }

        public int Distance { get; set; }
    }
}