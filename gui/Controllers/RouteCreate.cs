using db.Contexts;
using db.Models;
using gui.Classes;
using Microsoft.EntityFrameworkCore;

namespace gui.Controllers {
    public partial class RouteCreate : UserControl, IInformation {
        OrderDbContext _context;
        string User { get; set; }

        TextBox _boardingAddress, _dropAddress, _noteBox;
        DataGridView? _grid;

        public RouteCreate(DbContext context, string author, DataGridView? grid = null) {
            InitializeComponent();
            InitVariables();

            this._context = (OrderDbContext)context;
            this.User = author;
            _grid = grid;
        }

        private void enterBtn_Click(object sender, EventArgs e) {
            var route = new Route {
                BoardingAddress = _boardingAddress.Text,
                DropAddress = _dropAddress.Text,
                WhoAdded = User,
                WhenAdded = DateTime.Now,
                Note = _noteBox.Text
            };

            var validation = Validate(route);
            if (!validation.isValid) {
                MessageBox.Show(validation.Errors[0], IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _context.Routes.Add(route);
            EFCoreConnect.ApplyChangesToDatabase(_context);

            if (_grid == null) {
                _grid = this.Parent.Controls.OfType<DataGridView>().FirstOrDefault();
            }

            _grid?.DataSource = _context.Orders.ToList();
            _grid?.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
        }

        private void roundedButton1_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        #region Пользовательские функции

        void InitVariables() {
            _boardingAddress = this.boardingAddressTxtBox;
            _dropAddress = this.dropAddressTxtBox;
            _noteBox = this.noteTxtBox;
        }

        Classes.ValidationResult Validate(Route route) {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(route.BoardingAddress)) {
                errors.Add("Введите адрес посадки");
            } else if (string.IsNullOrEmpty(route.DropAddress)) {
                errors.Add("Введите адрес высадки");
            }

            return new Classes.ValidationResult(errors.Count == 0, errors);
        }

        #endregion
    }
}
