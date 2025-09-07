using db.Interfaces;
using gui.Controllers;

namespace gui.Forms {
    public partial class Login : Form {
        private Control? actionControl;

        private const string AUTHORIZE_NAME = "Авторизация";
        private const string REGISTER_NAME = "Регистрация";

        public enum ActionType { Authorize, Register }

        public Login() {
            InitializeComponent();
            InitVariables();

            this.SuspendLayout();
            this.Size = new Size(this.Size.Width, actionStrip.Height + actionControl.Height);
            this.ResumeLayout();
            this.Update();
        }

        private void actionStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            // Whether incorrect item clicked
            if (e.ClickedItem.Text != AUTHORIZE_NAME && e.ClickedItem.Text != REGISTER_NAME)
                return;

            Control control = actionControl; // save buffer for controls
            this.SuspendLayout(); // stop drawing until make new controller
            if (e.ClickedItem.Text == AUTHORIZE_NAME && (ActionType)actionControl.Tag != ActionType.Authorize) {
                actionControl = MakeActionControl(ActionType.Authorize);
            } else if (e.ClickedItem.Text == REGISTER_NAME && (ActionType)actionControl.Tag != ActionType.Register) {
                actionControl = MakeActionControl(ActionType.Register);
            } else {
                return;
            }

            if (actionControl == null) {
                actionControl = control;
                MessageBox.Show($"Ошибка формирования окна {(e.ClickedItem.Text == AUTHORIZE_NAME ? "авторизации" : "регистрации")}",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            control.Dispose();

            this.Size = new Size(this.Size.Width, actionStrip.Height + actionControl.Height);
            this.Controls.Add(actionControl);
            this.ResumeLayout();
            this.Update();
        }

        #region Пользовательские методы

        private void InitVariables() {
            var authorizeControl = MakeActionControl(ActionType.Authorize);
            this.Controls.Add(authorizeControl);
            actionControl = authorizeControl;
        }

        private Control? MakeActionControl(ActionType type) {
            Control? control = type switch {
                ActionType.Authorize => new AuthorizeControl(),
                ActionType.Register => new RegisterControl(),
                _ => null
            };

            if (control == null)
                return null;
            control.Dock = DockStyle.Fill;
            control.BackColor = Color.White;

            return control;
        }

        #endregion
    }
}
