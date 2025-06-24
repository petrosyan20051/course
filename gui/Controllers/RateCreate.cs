using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using System.Data;

using TypeId = int;

namespace gui.Controllers {
    public partial class RateCreate : UserControl, IInformation {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _forenameBox, _driverId, _vehicleId, _movePriceBox, _idlePriceBox, _noteBox;
        DataGridView? _grid;

        public RateCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var rate = new Rate {
                Forename = _forenameBox.Text,
                DriverId = TypeId.TryParse(_driverId.Text, out var driver) ? driver : -1,
                VehicleId = TypeId.TryParse(_vehicleId.Text, out var vehicle) ? vehicle : -1,
                MovePrice = TypeId.TryParse(_movePriceBox.Text, out var movePrice) ? movePrice : -1,
                IdlePrice = TypeId.TryParse(_idlePriceBox.Text, out var idlePrice) ? idlePrice : -1,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(rate);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.Rates.Add(rate);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            if (_grid == null) {
                _grid = this.Parent.Controls.OfType<DataGridView>().FirstOrDefault();
            }

            _grid?.DataSource = _context.Orders.ToList();
            _grid?.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        #region Пользовательские функции

        void InitVariables() {
            _forenameBox = this.forenameTxtBox;
            _driverId = this.driverIdTxtBox;
            _vehicleId = this.vehicleIdTxtBox;
            _movePriceBox = this.movePriceTxtBox;
            _idlePriceBox = this.idlePriceTxtBox;
            _noteBox = this.noteTxtBox;
        }

        Classes.ValidationResult Validate(Rate rate) {
            var errors = new List<string>();

            if (rate.Forename == string.Empty) {
                errors.Add("Введите название тарифа");
            } else if (_context.Rates.Where(o => o.DriverId == rate.DriverId) == null) {
                errors.Add($"Водитель с ID = {_driverId.Text} не существует");
            } else if (_context.Rates.Where(o => o.VehicleId == rate.VehicleId) == null) {
                errors.Add($"Маршрут с ID = {_vehicleId.Text} не существует");
            } else if (rate.MovePrice <= 0) {
                errors.Add($"Цена поездки (руб./мин.) должна быть положительным целым числом");
            } else if (rate.IdlePrice <= 0) {
                errors.Add($"Цена ожидания подачи должна быть положительным целым числом");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion
    }
}
