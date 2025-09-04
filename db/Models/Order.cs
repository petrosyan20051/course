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

        //public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        //public ICollection<Route> Routes { get; set; } = new List<Route>();
        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}