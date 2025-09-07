using gui.Controllers;

namespace gui.Forms {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
            //InitVariables(); 

            this.SuspendLayout();

            var authorizeControl = new AuthorizeControl();
            authorizeControl.Dock = DockStyle.Fill;
            authorizeControl.BackColor = Color.White;
            this.Controls.Add(authorizeControl);

            this.Size = new Size(this.Size.Width, actionStrip.Height + authorizeControl.Height);
            this.ResumeLayout();
            this.Update();
        }

        #region Пользовательские методы

        /*private void InitVariables() {
            
        }*/

        #endregion        
    }
}
