using System.ComponentModel.DataAnnotations.Schema;

namespace db.Models {

    public class Order {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int IdCustomer { get; set; }

        [ForeignKey("Route")]
        public int IdRoute { get; set; }

        [ForeignKey("Rate")]
        public int IdRate { get; set; }

        public int Distance { get; set; }

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string WhoChanged { get; set; } = string.Empty;
        public DateTime WhenChanged { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}