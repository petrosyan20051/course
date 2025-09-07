using gui.Forms;
using gui.Classes;
using Microsoft.IdentityModel.Tokens;

namespace gui.Controllers {
    public partial class RegisterControl : UserControl {
        TextBox _loginBox, _passwordBox;
        ComboBox _roleBox;

        public RegisterControl() {
            InitializeComponent();
            InitVariables();
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            if (_loginBox.Text.IsNullOrEmpty()) {
                MessageBox.Show("Введите логин", IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } else if (_passwordBox.Text.IsNullOrEmpty()) {
                MessageBox.Show("Введите пароль", IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!PasswordHasher.IsPasswordStrong(_passwordBox.Text)) {
                MessageBox.Show($"Введенный пароль недопустим.{Environment.NewLine}" +
                    $"Пароль должен содержать как минимум:{Environment.NewLine}" +
                    $"1. Одну латинскую букву нижнего и верхнего регистра.{Environment.NewLine}" +
                    $"2. Одну цифру.{Environment.NewLine}" +
                    $"3. Один спецсимвол.{Environment.NewLine}" +
                    $"Длина пароль должен быть не менее 8 символов.",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #region Пользовательские методы

        private void InitVariables() {
            _loginBox = this.loginTxtBox;
            _passwordBox = this.passwordTxtBox;

            _roleBox = this.roleComboBox;
            _roleBox.SelectedItem = _roleBox.Items[0];

            this.Tag = Login.ActionType.Register;
        }

        #endregion
    }
}
