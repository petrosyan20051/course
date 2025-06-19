using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TypeId = int;

namespace gui.Forms {
    public partial class OrderForm : Form, IInformation {
        OrderDbContext _context;
        string User { get; set; }


        TextBox _customerId, _routeId, _rateId, _distanceBox, _noteBox;

        public OrderForm(DbContext context, string author) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var order = new Order {
                CustomerId = TypeId.TryParse(_customerId.Text, out var customer) ? customer : -1,
                RouteId = TypeId.TryParse(_routeId.Text, out var route) ? route : -1,
                RateId = TypeId.TryParse(_customerId.Text, out var rate) ? rate : 0,
                Distance = TypeId.TryParse(_customerId.Text, out var distance) ? distance : 0,
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

        }

        #region Пользовательские функции

        void InitVariables() {
            _customerId = this.customerTxtBox;
            _routeId = this.routeTxtBox;
            _rateId = this.rateTxtBox;
            _distanceBox = this.distanceTxtBox;
            _noteBox = this._noteBox;
        }

        Classes.ValidationResult Validate(Order order) {
            var errors = new List<string>();

            if (_context.Orders.AsEnumerable().First(o => o.CustomerId == order.CustomerId) == null) {
                errors.Add($"Заказчик с ID = {order.CustomerId} не существует");
            } else if (_context.Orders.AsEnumerable().First(o => o.RouteId == order.RouteId) == null) {
                errors.Add($"Маршрут с ID = {order.RouteId} не существует");
            } else if (_context.Orders.AsEnumerable().First(o => o.RateId == order.RateId) == null) {
                errors.Add($"Тариф с ID = {order.RateId} не существует");
            } else if (order.Distance <= 0) {
                errors.Add($"Расстояние маршрута должно быть положительным целым числом");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);                
        }
        #endregion


            }
}
