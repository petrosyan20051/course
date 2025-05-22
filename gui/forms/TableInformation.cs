using gui.classes;
using db.Models;
using Microsoft.EntityFrameworkCore;

namespace gui.forms {

    public partial class TableInformation : Form {
        private DataGridView _grid;
        private readonly OrderContext _context; // db context

        public TableInformation() {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;

            // Make instance db context
            var factory = new OrderContextFactory();
            _context = factory.CreateDbContext([]);
        }

        private void InitVariables() {
            _grid = this.dbGrid;
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;

            try {
                var orders = _context.Orders.ToList(); // get data about orders
                _grid.DataSource = orders; // bind data to grid
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}