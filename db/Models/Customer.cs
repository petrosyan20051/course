﻿using System.ComponentModel.DataAnnotations;

using TypeId = int;

namespace db.Models {

    public class Customer {

        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [Display(Order = 2)]
        public string Forename { get; set; } = string.Empty;
        [Display(Order = 3)]
        public string Surname { get; set; } = string.Empty;
        [Display(Order = 4)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Order = 5)]
        public string Email { get; set; } = string.Empty;

        [Display(Order = 6)]
        public string WhoAdded { get; set; } = string.Empty;
        [Display(Order = 7)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 8)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 9)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 10)]
        public string? Note { get; set; } = null;
        [Display(Order = 11)]
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Order> orders { get; set; } = new List<Order>();
    }
}