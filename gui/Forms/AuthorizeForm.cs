using gui.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using db.Factories;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;

namespace gui.Forms {
    public partial class AuthorizeForm : Form {
        private TextBox? _dbBox, _ipBox, _loginBox, _passwordBox;

        public AuthorizeForm() {
            InitializeComponent();
            InitVariables();

            _ipBox.Text = GetLocalIPAddress();
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            // Check whether all textboxes has text
            string? errorMsg = null;
            if (_ipBox.Text == string.Empty) {
                errorMsg = "Введите IP-адрес подключения";
            } else if (_dbBox.Text == string.Empty) {
                errorMsg = "Введите идентификатор базы данных";
            } else if (_loginBox.Text == string.Empty) {
                errorMsg = "Введите логин пользователя";
            } else if (_passwordBox.Text == string.Empty) {
                errorMsg = "Введите пароль пользователя";
            }

            if (errorMsg != null) {
                MessageBox.Show(errorMsg, IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create OrderDbContext 
            var dbContext = new OrderDbContextFactory().CreateCustomDbContext(
                new string[] { _ipBox.Text, _dbBox.Text, _loginBox.Text, _passwordBox.Text });
            if (!dbContext.Database.CanConnect()) {
                MessageBox.Show(
                    $"Не удалось подключиться к базе данных.{Environment.NewLine}" +
                    $"Проверьте настройки подключения",
                    IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            // Tag as array of dbcontext and whether user is admin
            this.Tag = new object[] { dbContext, Tools.CheckSqlServerPermissionsForAdmin(dbContext as DbContext, _loginBox.Text) };
            MessageBox.Show($"Подключение к базе данных {_dbBox.Text} прошло успешно{Environment.NewLine}", 
                IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

        #region Пользовательские методы

        private void InitVariables() {
            _dbBox = this.dbNameTxtBox;
            _ipBox = this.ipTxtBox;
            _loginBox = this.userNameTxtBox;
            _passwordBox = this.passwordTxtBox;
        }

        private string? GetLocalIPAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName()); // get all net interfaces
            return host.AddressList
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)?
                .FirstOrDefault()?
                .ToString();
        }

        #endregion
    }
}
