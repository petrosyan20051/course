using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TypeId = int;

namespace gui.Controllers {
    public partial class TransportVehicleCreate : UserControl, IInformation {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _driverId, _numberBox, _seriesBox, _registrationCodeBox, _modelBox, _colorBox, _releaseYearBox, _noteBox;
        DataGridView? _grid;

        public TransportVehicleCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var vehicle = new TransportVehicle {
                DriverId = TypeId.TryParse(_driverId.Text, out var driver) ? driver : -1,
                Number = _numberBox.Text,
                Series = _seriesBox.Text,
                RegistrationCode = TypeId.TryParse(_registrationCodeBox.Text, out var registrationCode) ? registrationCode : -1,
                Model = _modelBox.Text,
                Color = _colorBox.Text,
                ReleaseYear = _releaseYearBox.Text,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(vehicle);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.TransportVehicles.Add(vehicle);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            if (_grid == null) {
                _grid = this.Parent.Controls.OfType<DataGridView>().FirstOrDefault();
            }

            _grid?.DataSource = _context.Orders.ToList();
            _grid?.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        #region Пользовательские функции

        void InitVariables() {
            _driverId = this.driverIdTxtBox;
            _numberBox = this.numberTxtBox;
            _seriesBox = this.seriesTxtBox;
            _registrationCodeBox = this.registrationCodeTxtBox;
            _modelBox = this.modelTxtBox;
            _colorBox = this.colorTxtBox;
            _releaseYearBox = this.releaseYearTxtBox;
            _noteBox = this.noteTxtBox;
        }

        Classes.ValidationResult Validate(TransportVehicle vehicle) {
            var errors = new List<string>();

            if (_context.Drivers.Where(o => o.Id == vehicle.DriverId) == null) {
                errors.Add($"Водитель с ID = {_driverId.Text} не существует");
            } else if (vehicle.Number.Length != 6) {
                errors.Add("Длина номера транспортного средства должна быть равна 6");
            } else if (!(vehicle.Number[0] >= 'A' && vehicle.Number[0] <= 'Z') ||
                !(vehicle.Number[1] >= '0' && vehicle.Number[1] <= '9') ||
                !(vehicle.Number[2] >= '0' && vehicle.Number[2] <= '9') ||
                !(vehicle.Number[3] >= '0' && vehicle.Number[3] <= '9') ||
                !(vehicle.Number[4] >= 'A' && vehicle.Number[4] <= 'Z') ||
                !(vehicle.Number[5] >= 'A' && vehicle.Number[5] <= 'Z')) {
                errors.Add($"Номер транспортного средства должен соответствовать шаблону: \"БЦЦЦББ\", где{Environment.NewLine}" +
                    $"\tБ - латинская заглавная буква{Environment.NewLine}" +
                    $"\tЦ - арабская цифра");
            } else if (vehicle.Series.Length != 1 || !(vehicle.Series[0] >= 'A' && vehicle.Number[0] <= 'Z')) {
                errors.Add("Серия должна содержать только заглавную латинскую букву");
            } else if (vehicle.RegistrationCode < 0) {
                errors.Add($"Код регистрации должен быть натуральным числом в диапазоне [1; 999]");
            } else if (string.IsNullOrEmpty(vehicle.Model)) {
                errors.Add("Введите модель транспортного средства");
            } else if (string.IsNullOrEmpty(vehicle.Color)) {
                errors.Add("Введите цвет транспортного средства");
            } else if (!TypeId.TryParse(vehicle.ReleaseYear, out var year) || year < 1800 || year >= 2025) {
                errors.Add("Код выпуска должен быть натуральным числом в диапазоне [1800; 2025]");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion
    }
}
