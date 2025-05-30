using db.Contexts;
using db.Factories;
using db.Models;
using db.Repositories;
using gui.classes;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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
        private Button _addSetBtn;

        private OrderDbContext _context; // db context

        public TableInformation(UserRights userRights = UserRights.Admin) {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;
            this.Rights = userRights;

            // Combobox source: OrderDbContext table names
            _tblCmBox.DataSource = Tools.GetTableNames(_context);

            // Get source for datagridview
            try {
                _grid.DataSource = Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]);
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Tools.ReorderColumnsAccordingToDbContext<Order>(_grid); // reorder columns
            _grid.AutoResizeColumns(); // resize columns

            _grid.ReadOnly = userRights == UserRights.Admin ? false : true; // install right to edit db
            _addSetBtn.Enabled = userRights == UserRights.Admin ? true : false; // install right to edit db
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;
        }

        private void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox? cmbBox = sender as ComboBox;
            var dbSet = Tools.GetDbSet(_context, tableMapping[cmbBox.Text]);
            _grid.DataSource = Tools.DbSetFilterByRole(dbSet, Rights, tableMapping[cmbBox.Text]);
            Tools.ReorderColumnsAccordingToDbContextByType(_grid, tableMapping[cmbBox.Text]); // reorder columns
        }

        private async void addSetBtn_Click(object sender, EventArgs e) {
            OrderRepository rep = new OrderRepository(_context);
            await rep.AddAsync(new Order());

            _grid.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];

            //_context.Orders.AddAsync(new Order());
            //_grid.Rows.Add(); // add new empty row

        }

        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;
            _addSetBtn = this.addSetBtn;

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
                    Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]) as IList,
                    Rights,
                    tableMapping[_tblCmBox.Text]);
            Tools.ReorderColumnsAccordingToDbContextByType(_grid, tableMapping[_tblCmBox.Text]); // reorder columns

            if (newRights != UserRights.Admin) {
                _grid.ReadOnly = true; // user is not admin so can't edit grid
                Tools.HideColumnsFromDataGridView(_grid, ["isDeleted"]);
                _addSetBtn.Enabled = false; // install right to edit db
            } else {
                _grid.ReadOnly = false; // user is admin
                Tools.ShowUpColumnsFromDataGridView(_grid, ["isDeleted"]);
                _addSetBtn.Enabled = true; // install right to edit db
            }
        }

        #endregion Пользовательские методы
    }
}