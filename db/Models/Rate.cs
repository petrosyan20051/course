﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TypeId = int;

namespace db.Models {

    public class Rate {

        [Key]
        [Display(Order = 1)]
        public TypeId Id { get; set; }

        [Display(Order = 2)]
        public required string Forename { get; set; }

        [ForeignKey("Driver")]
        [Display(Order = 3)]
        public TypeId DriverId { get; set; }

        [ForeignKey("Vehicle")]
        [Display(Order = 4)]
        public TypeId VehicleId { get; set; }

        [Display(Order = 5)]
        public int MovePrice { set; get; }
        [Display(Order = 6)]
        public int IdlePrice { set; get; }

        [Display(Order = 7)]
        public required string WhoAdded { get; set; }
        [Display(Order = 8)]
        public DateTime WhenAdded { get; set; }
        [Display(Order = 9)]
        public string? WhoChanged { get; set; } = null;
        [Display(Order = 10)]
        public DateTime? WhenChanged { get; set; } = null;
        [Display(Order = 11)]
        public string? Note { get; set; } = null;
        [Display(Order = 12)]
        public DateTime? isDeleted { get; set; } = null;

        //public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}