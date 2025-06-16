using db.Factories;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using static db.Custom_Classes.SqlConnect;

namespace gui.Forms {
    public partial class AuthorizeForm : Form {
        private TextBox? _loginBox, _passwordBox;
        private ComboBox _serverNameBox, _dbNameBox, _secBox;

        public AuthorizeForm() {
            InitializeComponent();
            InitVariables();

            _secBox.Text = _secBox.Items[0]?.ToString();
        }

        private void enterBtn_Click(object sender, EventArgs e) {
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

            // Create OrderDbContext 
            var dbContext = new OrderDbContextFactory().CreateCustomDbContext(
                new string[] { _loginBox.Enabled ? ((int)ConnectMode.SqlServerSecure).ToString() : ((int)ConnectMode.WindowsSecure).ToString(),
                    _serverNameBox.Text, _dbNameBox.Text, _loginBox.Text, _passwordBox.Text });
            if (!dbContext.Database.CanConnect()) {
                MessageBox.Show(
                    $"Не удалось подключиться к базе данных.{Environment.NewLine}" +
                    $"Проверьте настройки подключения",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tag:
            //  1. OrderDbContext
            //  2. Whether user is admin (bool)
            //  3. User's name (returns "Локальная БД" if local db)
            this.Tag = new object[] { dbContext,
                EFCoreConnect.CheckSqlServerPermissionsForAdmin(dbContext as DbContext, _loginBox.Text),
                (_loginBox.Enabled ? _loginBox.Text : "Локальная БД")};
            MessageBox.Show($"Подключение к базе данных {_dbNameBox.Text} прошло успешно{Environment.NewLine}",
                IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

        #region Пользовательские методы

        private void InitVariables() {
            _dbNameBox = this.dbNameBox;
            _serverNameBox = this.servNameBox;
            _loginBox = this.userNameTxtBox;
            _passwordBox = this.passwordTxtBox;
            _secBox = this.secCheckBox;
        }

        #endregion

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
    }
}
