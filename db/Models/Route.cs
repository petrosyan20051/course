namespace db.Models {

    public class Route : BaseModel {
        public string BoardingAddress { get; set; } = string.Empty;
        public string DropAddress { get; set; } = string.Empty;

        //public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}