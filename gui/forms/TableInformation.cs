using db.Contexts;
using db.Factories;
using gui.classes;

namespace gui.forms {

    public partial class TableInformation : Form {
        private DataGridView _grid;
        private ComboBox _tblCmBox;

        private readonly OrderDbContext _context; // db context

        public TableInformation() {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;

            // Make instance db context
            var factory = new OrderContextFactory();
            _context = factory.CreateDbContext([]);

            // Get source for datagridview
            try {
                var orders = _context.Orders.ToList(); // get data about orders
                _grid.DataSource = orders; // bind data to grid
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _grid.AutoResizeColumns(); // resize columns
            _tblCmBox.DataSource = Tools.GetTableNames<OrderDbContext>(_context);
        }

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;
        }

        private void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
        }
    }
}