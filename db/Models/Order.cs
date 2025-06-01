using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class Order : BaseModel {
        [ForeignKey("Customer")]
        public TypeId CustomerId { get; set; }

        [ForeignKey("Route")]
        public TypeId RouteId { get; set; }

        [ForeignKey("Rate")]
        public TypeId RateId { get; set; }

        public int Distance { get; set; }
    }
}