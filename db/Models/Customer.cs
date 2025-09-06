using System;
using System.ComponentModel.DataAnnotations;

using TypeId = int;

namespace db.Models {

    public class Customer {
        
        [Key]
        public TypeId Id { get; set; }

        public string Forename { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string WhoAdded { get; set; } = string.Empty;
        public DateTime WhenAdded { get; set; }
        public string? WhoChanged { get; set; } = null;
        public DateTime? WhenChanged { get; set; } = null;
        public string? Note { get; set; } = null;
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}