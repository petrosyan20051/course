using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using System.Data;

using TypeId = int;

namespace gui.Controllers {
    public partial class OrderCreate : UserControl, IInformation {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _customerId, _routeId, _rateId, _distanceBox, _noteBox;
        DataGridView? _grid;

        public OrderCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var order = new Order {
                CustomerId = TypeId.TryParse(_customerId.Text, out var customer) ? customer : -1,
                RouteId = TypeId.TryParse(_routeId.Text, out var route) ? route : -1,
                RateId = TypeId.TryParse(_customerId.Text, out var rate) ? rate : -1,
                Distance = TypeId.TryParse(_customerId.Text, out var distance) ? distance : -1,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(order);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.Orders.Add(order);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            if (_grid != null) {
                _grid = this.Parent.Controls.OfType<DataGridView>().FirstOrDefault();
            }
                
            _grid.DataSource = _context.Orders.ToList();
            _grid.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        #region Пользовательские функции

        void InitVariables() {
            _customerId = this.customerTxtBox;
            _routeId = this.routeTxtBox;
            _rateId = this.rateTxtBox;
            _distanceBox = this.distanceTxtBox;
            _noteBox = this.noteTxtBox;
        }

        Classes.ValidationResult Validate(Order order) {
            var errors = new List<string>();

            if (_context.Orders.Where(o => o.CustomerId == order.CustomerId) == null) {
                errors.Add($"Заказчик с ID = {order.CustomerId} не существует");
            } else if (_context.Orders.Where(o => o.RouteId == order.RouteId) == null) {
                errors.Add($"Маршрут с ID = {order.RouteId} не существует");
            } else if (_context.Orders.Where(o => o.RateId == order.RateId) == null) {
                errors.Add($"Тариф с ID = {order.RateId} не существует");
            } else if (order.Distance <= 0) {
                errors.Add($"Расстояние маршрута должно быть положительным целым числом");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion
    }
}
