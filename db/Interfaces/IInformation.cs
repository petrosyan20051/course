namespace db.Interfaces {
    public interface IInformation {
        public enum UserRights { Basic, Editor, Admin } // rights access for user
        public enum RegisterType { Anonymous, Admin } //  depends on who register new user

        public static string AppName = "Курсовая работа";
    }
}

