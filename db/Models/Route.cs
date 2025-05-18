namespace db.Models {

    public class Route {
        public int Id { get; set; }
        public string BoardingAddress { get; set; } = string.Empty;
        public string DropAddress { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string WhoChanged { get; set; } = string.Empty;
        public DateTime WhenChanged { get; set; }
        public string Note { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}