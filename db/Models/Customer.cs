namespace db.Models {

    public class Customer : BaseModel {
        public int Id { get; set; }
        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}