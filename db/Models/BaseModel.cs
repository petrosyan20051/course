using db.Tools;

using TypeId = int;

namespace db.Models {

    public abstract class BaseModel {

        [DisplayPriority(true)]
        public TypeId Id { get; set; }

        [DisplayPriority(false)]
        public string WhoAdded { get; set; } = string.Empty;

        [DisplayPriority(false)]
        public DateTime WhenAdded { get; set; }

        [DisplayPriority(false)]
        public string? WhoChanged { get; set; } = null;

        [DisplayPriority(false)]
        public DateTime? WhenChanged { get; set; } = null;

        [DisplayPriority(false)]
        public string? Note { get; set; } = null;

        [DisplayPriority(false)]
        public DateTime? isDeleted { get; set; } = null;
    }
}