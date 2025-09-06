using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class Order {

        [Key]
        public TypeId Id { get; set; }

        [ForeignKey("Customer")]
        public TypeId CustomerId { get; set; }

        [ForeignKey("Route")]
        public TypeId RouteId { get; set; }

        [ForeignKey("Rate")]
        public TypeId RateId { get; set; }

        public int Distance { get; set; }

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Customer> Customers { get; set; } = new List<Customer>();
        //public ICollection<Route> Routes { get; set; } = new List<Route>();
        //public ICollection<Rate> Rates { get; set; } = new List<Rate>();
    }
}