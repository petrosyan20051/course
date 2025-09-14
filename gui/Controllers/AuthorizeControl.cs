using db.Contexts;
using db.Factories;
using gui.Classes;
using gui.Forms;
using static db.Custom_Classes.SqlConnect;

namespace gui.Controllers {
    public partial class AuthorizeControl : UserControl {
        private TextBox? _loginBox, _passwordBox;
        private ComboBox _serverNameBox, _dbNameBox, _secBox;

        const string DEFAULTDBNAME = "KR";
        private readonly string[] SECURITY = { "Проверка подлинности Windows", "Проверка подлинности SQL Server" };

        public AuthorizeControl() {
            InitializeComponent();
            InitVariables();
        }

        private async void enterBtn_Click(object sender, EventArgs e) {
            // Check whether all textboxes has text
            string? errorMsg = null;
            if (_serverNameBox.Enabled && _serverNameBox.Text == string.Empty) {
                errorMsg = "Введите имя сервера";
            } else if (_dbNameBox.Text == string.Empty) {
                errorMsg = "Введите имя базы данных";
            } else if (_loginBox.Enabled && _loginBox.Text == string.Empty) {
                errorMsg = "Введите логин пользователя";
            } else if (_passwordBox.Enabled && _passwordBox.Text == string.Empty) {
                errorMsg = "Введите пароль пользователя";
            }

            if (errorMsg != null) {
                MessageBox.Show(errorMsg, IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            progressBar.Visible = true;

            // Create OrderDbContext 
            OrderDbContext context = null;
            if (_secBox.Text == SECURITY[0]) {
                context = new OrderDbContextFactory().CreateDbContext([]);
            } else if (_secBox.Text == SECURITY[1]) {
                string connectMode = _loginBox.Enabled ? ((int)ConnectMode.SqlServerSecure).ToString() : ((int)ConnectMode.WindowsSecure).ToString();
                context = new OrderDbContextFactory().CreateCustomDbContext(
                    new string[] { connectMode, _serverNameBox.Text, _dbNameBox.Text, _loginBox.Text, _passwordBox.Text });
            }

            // Try to connect to db
            bool connect = false;
            await Task.Run(() => connect = context.Database.CanConnect());
            progressBar.Visible = false;
            if (!connect) {
                MessageBox.Show(
                    $"Не удалось подключиться к базе данных.{Environment.NewLine}" +
                    $"Проверьте настройки подключения",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AuthService service = new AuthService();
            var response = await service.LoginAsync(_loginBox.Text, _passwordBox.Text);

            /*// Check whether user exists
            var user = context.Credentials.FirstOrDefault(c => c.Username == _loginBox.Text);
            if (user == null && _secBox.Text == SECURITY[1]) {
                MessageBox.Show($"Пользователь с именем \"{_loginBox.Text}\" не существует.{Environment.NewLine}" +
                    $"Проверьте настройки подключения.",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                context.Database.CloseConnection();
                context.Dispose();
                return;
            }

            // Check whether user's password is correct
            if (!PasswordHasher.VerifyPassword(_passwordBox.Text, user?.Password) && _secBox.Text == SECURITY[1]) {
                MessageBox.Show($"Введён неверный пароль.{Environment.NewLine}" +
                    $"Проверьте настройки подключения.",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                context.Database.CloseConnection();
                context.Dispose();
                return;
            }*/

            // Tag:
            //  1. OrderDbContext
            //  2. User rights (IInformation.UserRights)
            //  3. User's name (returns "Локальная БД" if local db)
            this.Parent.Tag = new object[] {
                context,
                _secBox.Text == SECURITY[1],
                _loginBox.Enabled ? _loginBox.Text : "Локальная БД"
            };
            MessageBox.Show($"Подключение к базе данных {_dbNameBox.Text} прошло успешно{Environment.NewLine}",
                IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Parent.Dispose();
        }

        private void secCheckBox_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox box = sender as ComboBox;
            if (box?.Text == "Проверка подлинности Windows") {
                _loginBox.Enabled = false;
                _passwordBox.Enabled = false;
            } else {
                _loginBox.Enabled = true;
                _passwordBox.Enabled = true;
            }
        }

        #region Пользовательские методы

        private void InitVariables() {
            _dbNameBox = this.dbNameBox;
            _serverNameBox = this.servNameBox;
            _loginBox = this.userNameTxtBox;
            _passwordBox = this.passwordTxtBox;

            _secBox = this.secComboBox;
            _secBox.Text = _secBox.Items[0]?.ToString();


#if DEBUG

            _dbNameBox.Text = DEFAULTDBNAME;
            _serverNameBox.Text = "localhost";

#endif

            this.Tag = Login.ActionType.Authorize;
        }

        #endregion


    }
}
