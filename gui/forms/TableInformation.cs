using db.Contexts;
using db.Factories;
using db.Models;
using gui.classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace gui.forms {

    public partial class TableInformation : BaseForm {
        private Dictionary<string, Type> tableMapping;

        public override UserRights Rights {
            get => base.Rights;
            set {
                if (base.Rights != value) {
                    base.Rights = value;
                    OnUserRightsChanged(value, _tblCmBox.Text);
                }
            }
        }

        private DataGridView _grid;
        private ComboBox _tblCmBox;

        private OrderDbContext _context; // db context

        public TableInformation(UserRights userRights = UserRights.Admin) {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;
            this.Rights = userRights;

            // Combobox source: string + value
            _tblCmBox.DataSource = Tools.GetTableNames(_context);

            // Get source for datagridview
            try {
                _grid.DataSource = Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]);
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _grid.AutoResizeColumns(); // resize columns
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;
        }

        private void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox cmbBox = sender as ComboBox;
            _grid.DataSource =
                Tools.DbSetFilterByRole(
                    Tools.GetDbSet(_context, tableMapping[cmbBox.Text]),
                    Rights,
                    tableMapping[cmbBox.Text]);
        }

        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;

            // Make instance db context
            var factory = new OrderContextFactory();
            _context = factory.CreateDbContext([]);

            // Making mapping for tables: string TableName -> Type TableType
            tableMapping = new Dictionary<string, Type>();
            foreach (var entityType in _context.Model.GetEntityTypes()) {
                tableMapping.Add(entityType.GetTableName(), entityType.ClrType);
            }
        }

        private void OnUserRightsChanged(UserRights newRights, string dbSetName) {
            if (!tableMapping.ContainsKey(_tblCmBox.Text))
                return;
            _grid.DataSource =
                Tools.DbSetFilterByRole(
                    Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]),
                    Rights,
                    tableMapping[_tblCmBox.Text]);
        }

        #endregion Пользовательские методы
    }
}