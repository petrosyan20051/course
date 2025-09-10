using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;

namespace gui.Controllers {
    public partial class DriverCreate : UserControl, IInformation {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _forenameBox, _surnameBox, _phoneNumberBox, _licenceSeriesBox, _licenceNumberBox, _noteBox;
        DataGridView? _grid;

        public DriverCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var driver = new Driver {
                Forename = _forenameBox.Text,
                Surname = _surnameBox.Text,
                PhoneNumber = _phoneNumberBox.Text,
                DriverLicenceSeries = _licenceSeriesBox.Text,
                DriverLicenceNumber = _licenceNumberBox.Text,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(driver);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.Drivers.Add(driver);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            if (_grid == null) {
                _grid = this.Parent.Controls.OfType<DataGridView>().FirstOrDefault();
            }

            _grid?.DataSource = _context.Drivers.ToList();
            _grid?.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        #region Пользовательские функции

        void InitVariables() {
            _surnameBox = this.surnameTxtBox;
            _forenameBox = this.forenameTxtBox;
            _phoneNumberBox = this.phoneNumberTxtBox;
            _licenceSeriesBox = this.licenceSeriesTxtBox;
            _licenceNumberBox = this.licenceNumberTxtBox;
            _noteBox = this.noteTxtBox;

        }

        Classes.ValidationResult Validate(Driver driver) {
            var errors = new List<string>();

            if (driver.Surname == string.Empty) {
                errors.Add("Введите фамилию водителя");
            } else if (driver.Forename == string.Empty) {
                errors.Add("Введите имя водителя");
            } else if (driver.PhoneNumber == string.Empty) {
                errors.Add("Введите номер телефона водителя");
            } else if (driver.DriverLicenceSeries == string.Empty) {
                errors.Add("Введите серию водительских прав");
            } else if (driver.DriverLicenceSeries.Length != 2) {
                errors.Add("Серия водительских прав должен содержать ровно 2 заглавных латинских буквы");
            } else if (driver.DriverLicenceNumber == string.Empty) {
                errors.Add("Введите номер водительских прав");
            } else if (driver.DriverLicenceNumber.Length != 6) {
                errors.Add("Номер водительских прав должен содержать ровно 6 цифр");
            } else if (!driver.DriverLicenceNumber.All(c => c >= '0' && c <= '9')) {
                errors.Add("Номер водительских прав может содержать только цифры");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion


    }
}
