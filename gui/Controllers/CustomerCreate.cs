using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;

namespace gui.Controllers {
    public partial class CustomerCreate : UserControl {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _forenameBox, _surnameBox, _phoneNumberBox, _emailBox, _noteBox;
        DataGridView? _grid;

        public CustomerCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var customer = new Customer {
                Forename = _forenameBox.Text,
                Surname = _surnameBox.Text,
                PhoneNumber = _phoneNumberBox.Text,
                Email = _emailBox.Text,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(customer);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.Customers.Add(customer);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            // Update grid in parent container (if null) otherwise using variable
            if (_grid == null) {
                _grid = this.Parent?.Controls?.OfType<DataGridView>().FirstOrDefault();
            }

            _grid.DataSource = _context.Customers.ToList();
            _grid.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        #region Пользовательские функции

        void InitVariables() {
            _emailBox = this.emailTxtBox;
            _forenameBox = this.forenameTxtBox;
            _phoneNumberBox = this.phoneTxtBox;
            _surnameBox = this.surnameTxtBox;
            _noteBox = this.noteTxtBox;
        }

        Classes.ValidationResult Validate(Customer customer) {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(customer.Surname)) {
                errors.Add("Введите фамилию заказчика");
            } else if (string.IsNullOrEmpty(customer.Forename)) {
                errors.Add("Введите имя заказчика");
            } else if (string.IsNullOrEmpty(customer.PhoneNumber)) {
                errors.Add("Введите номер телефона заказчика");
            } else if (string.IsNullOrEmpty(customer.Email)) {
                errors.Add("Введите адрес электронной почты заказчика");
            } else if (!customer.Email.Contains('@')) {
                errors.Add("Введен некорректный адрес электронной почты");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion
    }
}
