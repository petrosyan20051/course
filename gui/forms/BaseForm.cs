// Base class which contains properties to change userRights
namespace gui.forms {

    public partial class BaseForm : Form {
        public virtual UserRights Rights { get; set; }

        public enum UserRights { Basic, Admin } // rights access for user

        public static string AppName = "Курсовая работа";

        public BaseForm() {
        }
    }
}