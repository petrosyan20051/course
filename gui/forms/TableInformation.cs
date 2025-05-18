using gui.classes;

namespace gui.forms {

    public partial class TableInformation : Form {
        private DataGridView _grid;

        public TableInformation() {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;
        }

        private void InitVariables() {
            _grid = this.dbGrid;
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;

            try {
                // Make instance db context
                using (var context = new db.Models.OrderContext()) {
                    var orders = context.Orders.ToList(); // get data about orders
                    _grid.DataSource = orders; // bind data to grid
                }
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}