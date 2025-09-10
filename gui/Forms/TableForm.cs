using db.Contexts;
using db.Factories;
using db.Interfaces;
using gui.Classes;
using gui.Factories;
using Microsoft.EntityFrameworkCore;
using static gui.Classes.IInformation;

using TypeId = int;

namespace gui.Forms {

    public partial class TableForm : Form, Classes.IInformation {
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

        private string? cellBeforeEdit; // tracking current table grid cell value before editing
        private string? cellAfterEdit; // tracking current table grid cell value after editing (stop editing cell value)
        private string currentTable; // tracking current combobox text
        private bool IsUpdatingCellValue { get; set; } = false;
        private Point? SelectedCell { get; set; }

        private DataGridView _grid;
        private ComboBox _tblCmBox;
        private ToolStripMenuItem _setAdd, _setDelete, _setRecover;

        private OrderDbContext _context; // db context

        public TableForm(UserRights userRights) {
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

        private async void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBox? cmbBox = sender as ComboBox;

            // Update grid + filter
            _grid.DataSource = EFCoreConnect.GetBindingListByEntityName(_context, cmbBox.Text);

            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin

            // Reorder columns
            switch (_tblCmBox.Text) {
                case "Drivers": Tools.ReorderGridColumns(_grid, "Driver"); break;
                case "Rates": Tools.ReorderGridColumns(_grid, "Rate"); break;
                case "TransportVehicles": Tools.ReorderGridColumns(_grid, "TransportVehicle"); break;
            }

            // Delete adding new set controller and splitter if it exists
            if (cmbBox.Text != currentTable) {
                dataPnl.Controls.Remove(dataPnl.Controls.OfType<UserControl>().FirstOrDefault()); // delete custom controller
                dataPnl.Controls.Remove(dataPnl.Controls.OfType<Splitter>().FirstOrDefault()); // delete splitter
            }
            currentTable = cmbBox.Text;
        }

        private void addSetStrip_Click(object sender, EventArgs e) {
            var control = EntityFactory.CreateEntityFormByName(_tblCmBox.Text, _context, (string)this.Tag);
            if (control == null) {
                MessageBox.Show($"Не удалось создать контроллер для добавления набора в таблицу {_tblCmBox.Text}",
                    Classes.IInformation.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            control.Dock = DockStyle.Right;
            control.BackColor = Color.Beige;

            var splitter = new Splitter();
            splitter.Dock = DockStyle.Right;
            splitter.Width = 5;
            splitter.BackColor = Color.Gray;

            dataPnl.Controls.Add(splitter); // add splitter
            dataPnl.Controls.Add(control); // add control
        }

        private void dbGrid_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            if (e.Exception is FormatException) {
                e.ThrowException = false; // dont throw exception

                _grid.CurrentCell = _grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                MessageBox.Show(
                    $"Некорректное значение \"{cellAfterEdit}\"",
                    AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dbGrid_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            cellAfterEdit = e.Value?.ToString();
        }

        private void dbGrid_CellEnter(object sender, DataGridViewCellEventArgs e) {
            DataGridView grid = (DataGridView)sender;
            try {
                cellBeforeEdit = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            } catch (Exception ex) {
                cellBeforeEdit = string.Empty;
            }

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
            DataGridView grid = sender as DataGridView;
            if (grid.Columns.Contains("WhenChanged") && cellBeforeEdit != grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
                grid.Rows[e.RowIndex].Cells["WhenChanged"].Value = DateTime.Now;
            EFCoreConnect.ApplyChangesToDatabase(_context);
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
            _grid.AutoGenerateColumns = true;
        }

        private void OnUserRightsChanged(UserRights newRights, string? dbSetName) {

            _grid.DataSource = EFCoreConnect.GetBindingListByEntityName(_context, _tblCmBox.Text);

            FilterColumnsByRights(); // hide "isDeleted" in case user is not admin
            //Tools.ReorderColumnsAccordingToDbContextByType(_grid, tableMapping[_tblCmBox.Text]); // reorder columns

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

        private async void DeleteSetsFromGrid(DataGridView? grid) {
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

            // Get IDeletable repository
            IDeletable<TypeId> repository = _tblCmBox.Text switch {
                "Customers" => new RepositoryFactory(_context).CreateDeletableRepository("Customer"),
                "Drivers" => new RepositoryFactory(_context).CreateDeletableRepository("Driver"),
                "Orders" => new RepositoryFactory(_context).CreateDeletableRepository("Order"),
                "Rates" => new RepositoryFactory(_context).CreateDeletableRepository("Rate"),
                "Routes" => new RepositoryFactory(_context).CreateDeletableRepository("Route"),
                "TransportVehicles" => new RepositoryFactory(_context).CreateDeletableRepository("TransportVehicle")
            };

            // Delete by id
            int startRow = grid.SelectedRows.Cast<DataGridViewRow>().Min(r => r.Index); // start index to redraw while recovering
            grid.SuspendLayout(); // stop redrawing
            foreach (var id in idsToDelete) {
                await repository.SoftDeleteAsync(id);
                grid.InvalidateRow(startRow++);
            }

            grid.ResumeLayout(); // resume redrawing
            grid.Refresh();
            grid?.CurrentCell = grid?.Rows[SelectedCell.Value.X].Cells[SelectedCell.Value.Y]; // update selected cell
            MessageBox.Show("Удаление прошло успешно.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void RecoverSetsFromGrid(DataGridView grid) {
            SelectedCell = new Point(grid.CurrentCell.RowIndex, grid.CurrentCell.ColumnIndex);

            // Get Id to deleted
            var idsToRecover = grid?.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Id"]?.Value is TypeId id)
                .Select(row => (TypeId)row.Cells["Id"].Value)
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


            // Get IRecovarable repository
            IRecovarable<TypeId> repository = _tblCmBox.Text switch {
                "Customers" => new RepositoryFactory(_context).CreateRecoverableRepository("Customer"),
                "Drivers" => new RepositoryFactory(_context).CreateRecoverableRepository("Driver"),
                "Orders" => new RepositoryFactory(_context).CreateRecoverableRepository("Order"),
                "Rates" => new RepositoryFactory(_context).CreateRecoverableRepository("Rate"),
                "Routes" => new RepositoryFactory(_context).CreateRecoverableRepository("Route"),
                "TransportVehicles" => new RepositoryFactory(_context).CreateRecoverableRepository("TransportVehicle")
            };

            // Recover by id
            int startRow = grid.SelectedRows.Cast<DataGridViewRow>().Min(r => r.Index); // start index to redraw while recovering
            grid.SuspendLayout(); // stop redrawing
            foreach (var id in idsToRecover) {
                await repository.RecoverAsync(id);
                grid.InvalidateRow(startRow++);
            }

            grid.ResumeLayout(); // resume redrawing
            grid.Refresh();
            grid?.CurrentCell = grid?.Rows[SelectedCell.Value.X].Cells[SelectedCell.Value.Y]; // update selected cell
            MessageBox.Show("Восстановление прошло успешно.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async void UpdateForm(DbContext context, UserRights userRights) {
            _context = context as OrderDbContext;
            if (_context == null) {
                return;
            }

            // Close previous connection to DB
            _context?.Database.CloseConnection();

            // Combobox source: OrderDbContext table names
            _tblCmBox.DataSource = EFCoreConnect.GetTableNames(_context)
                .Where(n => n != "Credentials")
                .ToList();

            // Get source for datagridview
            _grid.DataSource = EFCoreConnect.GetBindingListByEntityName(_context, _tblCmBox.Text);

            FilterColumnsByRights();

            // Reorder columns
            switch (_tblCmBox.Text) {
                case "Drivers": Tools.ReorderGridColumns(_grid, "Driver"); break;
                case "Rates": Tools.ReorderGridColumns(_grid, "Rate"); break;
                case "TransportVehicles": Tools.ReorderGridColumns(_grid, "TransportVehicle"); break;
            }


            _grid.AutoResizeColumns(); // resize columns

            // Install enability for controllers

            _grid.ReadOnly = Rights switch {
                UserRights.Basic => true,
                UserRights.Editor => false,
                UserRights.Admin => false
            }; // install rights to edit db

            _setAdd.Enabled = Rights switch {
                UserRights.Basic => false,
                UserRights.Editor => true,
                UserRights.Admin => true
            }; // install rights to add sets to db

            _setDelete.Enabled = Rights switch {
                UserRights.Basic => false,
                UserRights.Editor => false,
                UserRights.Admin => true
            }; // install rights to remove sets from db

            _setRecover.Enabled = Rights switch {
                UserRights.Basic => false,
                UserRights.Editor => false,
                UserRights.Admin => true
            };  // install rights to recover sets from db
        }

        private void FilterColumnsByRights() {
            // Hide rows where "isDeleted" != null for no admin  
            if (Rights != UserRights.Admin) {
                for (var i = 0; i < _grid.Rows.Count; ++i) {
                    // Check whether set is "deleted"
                    if (_grid.Rows[i].Cells["isDeleted"].Value != null &&
                        _grid.Rows[i].Cells["isDeleted"].Value != DBNull.Value) {
                        _grid.Rows[i].Visible = false;
                    }
                }
            }

            // Hide "isDeleted" column
            if (_grid.Columns.Contains("isDeleted")) {
                _grid.Columns["isDeleted"].Visible = Rights == UserRights.Admin;
            }
        }
        #endregion Пользовательские методы
    }
}