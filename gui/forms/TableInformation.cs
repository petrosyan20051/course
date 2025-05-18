using gui.classes;
using System;
using System.Windows.Forms;

//using

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
                using (var context = new ApplicationContext()) {
                    // var orders = context.
                }
            } catch (Exception ex) { }
        }
    }
}