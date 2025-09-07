using gui.Controllers;

namespace gui.Forms {
    public partial class Login : Form {
        private Control actionControl;

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
            if (e.ClickedItem.Text == AUTHORIZE_NAME && (ActionType)actionControl.Tag != ActionType.Authorize) {
                //MessageBox.Show("Вход в авторизацию");
            } else if (e.ClickedItem.Text == REGISTER_NAME && (ActionType)actionControl.Tag != ActionType.Register) {
                //MessageBox.Show("Вход в регистрацию");
            }
        }

        #region Пользовательские методы

        private void InitVariables() {
            var authorizeControl = new AuthorizeControl();
            authorizeControl.Dock = DockStyle.Fill;
            authorizeControl.BackColor = Color.White;
            this.Controls.Add(authorizeControl);
            actionControl = authorizeControl;
        }

        #endregion
    }
}
