namespace db.Models {

    public class Customer {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string WhoChanged { get; set; } = string.Empty;
        public DateTime WhenChanged { get; set; }
        public string Note { get; set; } = string.Empty;

        public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}