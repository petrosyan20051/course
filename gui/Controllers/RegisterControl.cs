using gui.Classes;
using gui.Forms;
using Microsoft.IdentityModel.Tokens;

namespace gui.Controllers {
    public partial class RegisterControl : UserControl {
        TextBox _loginBox, _passwordBox;
        ComboBox _roleBox;

        private static readonly string[] ROLES = ["Базовый пользователь", "Редактор", "Администратор"];

        public RegisterControl() {
            InitializeComponent();
            InitVariables();
        }

        private async void enterBtn_Click(object sender, EventArgs e) {
            if (_loginBox.Text.IsNullOrEmpty()) {
                MessageBox.Show("Введите логин", IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } else if (_passwordBox.Text.IsNullOrEmpty()) {
                MessageBox.Show("Введите пароль", IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            /*if (!PasswordHasher.IsPasswordStrong(_passwordBox.Text)) {
                MessageBox.Show($"Введенный пароль недопустим.{Environment.NewLine}" +
                    $"Пароль должен содержать как минимум:{Environment.NewLine}" +
                    $"1. Одну латинскую букву нижнего и верхнего регистра.{Environment.NewLine}" +
                    $"2. Одну цифру.{Environment.NewLine}" +
                    $"3. Один спецсимвол.{Environment.NewLine}" +
                    $"Длина пароля должен быть не менее 8 символов.",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/

            //AuthService service = new AuthService();
            //var response = await service.LoginAsync(_)


            /*// Make repository to work with db
            CredentialRepository repository = new CredentialRepository(context);
            Credential entity = new Credential {
                Username = _loginBox.Text,
                Password = PasswordHasher.HashPassword(_passwordBox.Text),
                Rights = _roleBox.Text == ROLES[0] ? UserRights.Basic :
                    _roleBox.Text == ROLES[1] ? UserRights.Editor : UserRights.Admin,
                WhoAdded = IInformation.DefaultDbSetMaker,
                WhenAdded = DateTime.Now,
                Note = null,
                WhoChanged = null,
                WhenChanged = null,
                isDeleted = null
            };*/

            MessageBox.Show("Регистрация прошла успешно. Можете войти в профиль.",
                IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //context.Database.CloseConnection();
            //context.Dispose();
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
