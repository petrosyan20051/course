using db.Classes;

namespace db.Models {
    public class Credential : BaseModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public IInformation.UserRights Rights { get; set; } 
    }
}
