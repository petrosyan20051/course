namespace gui.Classes {
    public class ValidationResult {
        public bool isValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationResult(bool isValid, List<string> errors) {
            this.isValid = isValid;
            this.Errors = errors;
        }
    }
}
