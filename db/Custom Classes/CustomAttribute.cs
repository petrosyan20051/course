namespace db.Tools {

    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayPriorityAttribute : Attribute {
        public bool IsHighPriority { get; } // true для свойств текущего класса

        public DisplayPriorityAttribute(bool isHighPriority = true) {
            IsHighPriority = isHighPriority;
        }
    }
}