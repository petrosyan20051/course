using db.Contexts;
using db.Models;
using gui.Classes;
using gui.Factories;
using Microsoft.EntityFrameworkCore;
using static gui.Classes.IInformation;

namespace gui.Forms {

    public partial class TableForm : Form, IInformation {
        Dictionary<string, Type> tableMapping;

        private UserRights _rights;
        public UserRights Rights {
            get => _rights;
            set {
                if (_rights != value) {
                    _rights = value;
                    OnUserRightsChanged(value, _tblCmBox.Text);
                }
            }
        }

        private string? currentCell; // tracking current table grid cell value
        private string currentTable; // tracking current combobox text
        private bool IsUpdatingCellValue { get; set; } = false;
        private Point? SelectedCell { get; set; }

        private DataGridView _grid;
        private ComboBox _tblCmBox;
        private ToolStripMenuItem _setAdd, _setDelete, _setRecover;

        private OrderDbContext _context; // db context

        public TableForm(UserRights userRights = UserRights.Error) {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;
            this.Rights = userRights;

            _grid.ReadOnly = true; // install right to edit db
            _setAdd.Enabled = false; // install right to add sets to db
            _setDelete.Enabled = false; // install right to remove sets from db
            _setRecover.Enabled = false;
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;
        }

        private void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox? cmbBox = sender as ComboBox;

            // Update grid + filter
            _grid.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[cmbBox.Text]);
            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin
            Tools.ReorderColumnsAccordingToDbContextByType(_grid, tableMapping[cmbBox.Text]); // reorder columns

            // Delete adding new set controller and splitter if it exists
            if (cmbBox.Text != currentTable) {
                dataPnl.Controls.Remove(dataPnl.Controls.OfType<UserControl>().FirstOrDefault()); // delete custom controller
                dataPnl.Controls.Remove(dataPnl.Controls.OfType<Splitter>().FirstOrDefault()); // delete splitter
            }
            currentTable = cmbBox.Text;
        }

        private void addSetStrip_Click(object sender, EventArgs e) {
            var control = EntityFactory.CreateEntityFormByName(_tblCmBox.Text, _context, (string)this.Tag);
            control.Dock = DockStyle.Right;
            control.BackColor = Color.Beige;

            var splitter = new Splitter();
            splitter.Dock = DockStyle.Right;
            splitter.Width = 5;
            splitter.BackColor = Color.Gray;

            dataPnl.Controls.Add(splitter); // add splitter
            dataPnl.Controls.Add(control); // add control

            // Making new entity
            /*Type entityType = tableMapping[_tblCmBox.Text];
            var newEntity = Activator.CreateInstance(entityType);

            _context?.Add(newEntity);
            ApplyChangesToDatabase(_context);

            _grid.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[_tblCmBox.Text]);
            _grid.Refresh();
            _grid.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];*/
        }

        private void dbGrid_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            if (e.Exception is FormatException) {
                e.ThrowException = false; // dont throw exception

                _grid.CurrentCell = _grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                MessageBox.Show(
                    $"Некорректное значение \"{currentCell}\"",
                    AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dbGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            currentCell = e.Value?.ToString();
        }

        private void dbGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            // If user - admin so paint background for soft deleted and not deleted sets
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            dynamic? entity = (sender as DataGridView)?
                .Rows[e.RowIndex]?
                .DataBoundItem as dynamic;
            var brushColor = entity?.isDeleted is null ? Design.IsSetExists : Design.SoftDeleteColor;

            using (var brush = new SolidBrush(brushColor)) {
                e?.Graphics?.FillRectangle(brush, e.CellBounds); // fill rect with brush

                using (var pen = new Pen(Color.Black)) {
                    e?.Graphics?.DrawRectangle(pen, e.CellBounds);
                }
            }

            e.PaintContent(e.ClipBounds);
            e?.Handled = true;
        }

        private void dbGrid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Delete) {
                return;
            }

            DeleteSetsFromGrid(sender as DataGridView); // delete sets from datagridview
        }


        private void dbGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            var grid = sender as DataGridView;
            EFCoreConnect.ApplyChangesToDatabase(_context);
            //grid.DataSource = EFCoreConnect.GetBindingListByEntityType(new OrderDbContextFactory().CreateDbContext([]), tableMapping[_tblCmBox.Text]);
        }

        private void setDeleteStrip_Click(object sender, EventArgs e) {
            DeleteSetsFromGrid(_grid);
        }

        private void setRecoverStrip_Click(object sender, EventArgs e) {
            RecoverSetsFromGrid(_grid);
        }


        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;
            _setAdd = this.setAddStrip;
            _setDelete = this.setDeleteStrip;
            _setRecover = this.setRecoverStrip;

            currentTable = _tblCmBox.Text;
        }

        private void OnUserRightsChanged(UserRights newRights, string? dbSetName) {

            if (tableMapping == null) {
                return;
            } else if (!tableMapping.ContainsKey(_tblCmBox.Text))
                return;
            _grid.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[_tblCmBox.Text]);

            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin
            Tools.ReorderColumnsAccordingToDbContextByType(_grid, tableMapping[_tblCmBox.Text]); // reorder columns

            if (newRights != UserRights.Admin) {
                _grid.ReadOnly = true; // user is not admin so can't edit grid


                // Set enability for controllers when rights are changed
                _setAdd.Enabled = false;
                _setDelete.Enabled = false;
                _setRecover.Enabled = false;
            } else {
                _grid.ReadOnly = false; // user is admin

                // Set enability for controllers when rights are changed
                _setAdd.Enabled = true;
                _setDelete.Enabled = true;
                _setRecover.Enabled = true;
            }

            _grid.Invalidate();
        }

        private void DeleteSetsFromGrid(DataGridView? grid) {
            SelectedCell = new Point(grid.CurrentCell.RowIndex, grid.CurrentCell.ColumnIndex);

            // Get Id to deleted
            var idsToDelete = grid?.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Id"]?.Value is int id)
                .Select(row => (int)row.Cells["Id"].Value)
                .ToList();

            // Make prompt for confirmation
            string prompt;
            if (idsToDelete?.Count == 1) {
                prompt = $"Вы действительно хотите удалить набор с ID = {idsToDelete.First()}?";
            } else {
                prompt = "Вы действительно хотите удалить все выделенные наборы?";
            }

            // Confirm deleting
            if (MessageBox.Show(prompt, AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            try {
                // Delete selected sets
                foreach (var id in idsToDelete) {
                    dynamic? entityToDelete = _context.Find(tableMapping[_tblCmBox.Text], new object[] { id });

                    // Update entities fields
                    entityToDelete?.WhenChanged = DateTime.Now;
                    entityToDelete?.isDeleted = DateTime.Now;
                    _context.Update(entityToDelete);
                }

                EFCoreConnect.ApplyChangesToDatabase(_context);

                // Update DataGridView
                grid?.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[_tblCmBox.Text]);
            } catch (Exception ex) {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin
            grid?.CurrentCell = grid?.Rows[SelectedCell.Value.X].Cells[SelectedCell.Value.Y]; // update selected cell
            MessageBox.Show("Удаление прошло успешно.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RecoverSetsFromGrid(DataGridView? grid) {
            SelectedCell = new Point(grid.CurrentCell.RowIndex, grid.CurrentCell.ColumnIndex);

            // Get Id to deleted
            var idsToRecover = grid?.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Id"]?.Value is int id)
                .Select(row => (int)row.Cells["Id"].Value)
                .ToList();

            // Make prompt for confirmation
            string prompt;
            if (idsToRecover?.Count == 1) {
                prompt = $"Вы действительно хотите восстановить набор с ID = {idsToRecover.First()}?";
            } else {
                prompt = "Вы действительно хотите восстановить все выделенные наборы?";
            }

            // Confirm recovering
            if (MessageBox.Show(prompt, AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            try {
                // Recover selected sets
                foreach (var id in idsToRecover) {
                    dynamic? entityToRecover = _context.Find(tableMapping[_tblCmBox.Text], new object[] { id });

                    // Update entities fields
                    entityToRecover?.WhenChanged = DateTime.Now;
                    entityToRecover?.isDeleted = null;
                    _context.Update(entityToRecover);
                }

                EFCoreConnect.ApplyChangesToDatabase(_context);

                // Update DataGridView
                grid?.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[_tblCmBox.Text]);

            } catch (Exception ex) {
                MessageBox.Show($"Ошибка при восстановлении: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin
            grid?.CurrentCell = grid?.Rows[SelectedCell.Value.X].Cells[SelectedCell.Value.Y]; // update selected cell
            MessageBox.Show("Восстановление прошло успешно.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateForm(DbContext context, UserRights? userRights = null) {
            _context = context as OrderDbContext;
            if (_context == null) {
                return;
            }

            // Update user rights
            if (userRights != null) {
                Rights = (UserRights)userRights;
            }

            // Making mapping for tables: string TableName -> Type TableType
            if (tableMapping != null && tableMapping.Count > 0) {
                tableMapping.Clear();
            }
            tableMapping = new Dictionary<string, Type>();
            foreach (var entityType in _context.Model.GetEntityTypes()) {
                tableMapping.Add(entityType.GetTableName(), entityType.ClrType);
            }

            // Combobox source: OrderDbContext table names
            _tblCmBox.DataSource = EFCoreConnect.GetTableNames(_context);

            // Get source for datagridview
            try {
                _grid.DataSource = EFCoreConnect.GetBindingListByEntityType(_context, tableMapping[_tblCmBox.Text]);
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Tools.ReorderColumnsAccordingToDbContext<Order>(_grid); // reorder columns
            _grid.AutoResizeColumns(); // resize columns

            // Install enability for controllers

            _grid.ReadOnly = Rights == UserRights.Admin ? false : true; // install right to edit db
            _setAdd.Enabled = Rights == UserRights.Admin ? true : false; // install right to add sets to db
            _setDelete.Enabled = Rights == UserRights.Admin ? true : false; // install right to remove sets from db
            _setRecover.Enabled = Rights == UserRights.Admin ? true : false; // install right to recover sets from db

        }

        private void FilterColumnsByRights() {
            if (_grid.Columns.Contains("isDeleted")) {
                _grid.Columns["isDeleted"].Visible = Rights == UserRights.Admin;
            }
        }
        #endregion Пользовательские методы
    }
}