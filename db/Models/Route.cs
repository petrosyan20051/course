namespace db.Models {

    public class Route {
        public int Id { get; set; }
        public string BoardingAddress { get; set; } = string.Empty;
        public string DropAddress { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}