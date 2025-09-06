using System.ComponentModel.DataAnnotations;
using TypeId = int;

namespace db.Models {

    public abstract class BaseModel {

        [Key]
        public TypeId Id { get; set; }

        public string WhoAdded { get; set; } = string.Empty;

        public DateTime WhenAdded { get; set; }

        public string? WhoChanged { get; set; } = null;

        public DateTime? WhenChanged { get; set; } = null;

        public string? Note { get; set; } = null;

        public DateTime? isDeleted { get; set; } = null;
    }
}