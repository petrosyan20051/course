using db.Contexts;
using db.Factories;
using db.Models;
using db.Repositories;
using gui.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static gui.Classes.IInformation;

using TypeId = int;

namespace gui.Forms {

    public partial class Table : Form, IInformation {
        private Dictionary<string, Type> tableMapping;

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

        private string? currentCell;
        private bool IsUpdatingCellValue { get; set; } = false;

        private DataGridView _grid;
        private ComboBox _tblCmBox;
        private ToolStripMenuItem _setAdd, _setDelete;

        private OrderDbContext _context; // db context

        public Table(UserRights userRights = UserRights.Admin) {
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
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Tools.ReorderColumnsAccordingToDbContext<Order>(_grid); // reorder columns
            _grid.AutoResizeColumns(); // resize columns

            _grid.ReadOnly = userRights == UserRights.Admin ? false : true; // install right to edit db
            _setAdd.Enabled = userRights == UserRights.Admin ? true : false; // install right to add sets to db
            _setDelete.Enabled = userRights == UserRights.Admin ? true : false; // install right to remove sets from db

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

        private async void addSetStrip_Click(object sender, EventArgs e) {
            try {
                // Get DbSet<entityType> 
                var dbSetMethod = typeof(OrderDbContext)?.GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(tableMapping[_tblCmBox.Text]);
                var dbSet = dbSetMethod?.Invoke(_context, null); // DbSet<>

                // Make entity instance
                var entityInstance = Activator.CreateInstance(tableMapping[_tblCmBox.Text]);

                // Get new ID with repos
                var repository = Tools.GetRepositoryByName(_context, tableMapping[_tblCmBox.Text]);
                var newId = await repository?.NewIdToAdd();
                if (newId == -1) {
                    MessageBox.Show(
                        $"Невозможно получить новый ID.{Environment.NewLine}Все ID заняты",
                        AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set Id to instance
                tableMapping[_tblCmBox.Text].GetProperty("Id")?.SetValue(entityInstance, newId);
                _context.Add(entityInstance);

                await ApplyChangesToDatabase(_context);
            } catch (Exception error) {
                MessageBox.Show($"Произошла ошибка: {error.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _grid.DataSource = Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]);
            _grid.Refresh();
            _grid.CurrentCell = _grid.Rows[_grid.Rows.Count - 1].Cells[0];
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

        private async void dbGrid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Delete) {
                return;
            }

            await DeleteSetsFromGrid(sender as DataGridView); // delete sets from datagridview
        }


        private async void dbGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            var grid = sender as DataGridView;
            await ApplyChangesToDatabase(_context);
            grid.DataSource = Tools.GetDbSet(new OrderContextFactory().CreateDbContext([]), tableMapping[_tblCmBox.Text]);
        }

        private async void setDeleteStrip_Click(object sender, EventArgs e) {
            await DeleteSetsFromGrid(_grid);
        }

        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;
            _setAdd = this.setAddStrip;
            _setDelete = this.setDeleteStrip;

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
                _setAdd.Enabled = false; // install right to edit db
                _setDelete.Enabled = false; // install right to edit db
            } else {
                _grid.ReadOnly = false; // user is admin
                Tools.ShowUpColumnsFromDataGridView(_grid, ["isDeleted"]);
                _setAdd.Enabled = true; // install right to edit db
                _setDelete.Enabled = true; // install right to edit db
            }

            _grid.Invalidate();
        }

        private async Task ApplyChangesToDatabase(OrderDbContext _context) {
            try {
                await _context.SaveChangesAsync();
                // Updata Data Source

                //grid.DataSource = Tools.GetDbSet(_context, selectBox.Text == "Orders" ? typeof(Order) : typeof(Customer));
            } catch (DbUpdateException ex) {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.InnerException?.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (InvalidDataException ex) {
                MessageBox.Show($"Некорректные данные: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Microsoft.Data.SqlClient.SqlException ex) {
                MessageBox.Show($"Ошибка SQL: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (ArgumentNullException ex) {
                MessageBox.Show($"Аргумент не может быть null: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (InvalidOperationException ex) {
                MessageBox.Show($"Операция недопустима: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task DeleteSetsFromGrid(DataGridView? grid) {
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

            dynamic? repository = Tools.GetRepositoryByName(_context, tableMapping[_tblCmBox.Text]);
            try {
                // Delete selected sets
                foreach (var id in idsToDelete) {
                    await repository?.SoftDeleteAsync(id);
                }

                await ApplyChangesToDatabase(_context);

                // Update DataGridView
                grid?.DataSource = Tools.DbSetFilterByRole(
                    Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]),
                    Rights, tableMapping[_tblCmBox.Text]);

                MessageBox.Show("Удаление прошло успешно.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Пользовательские методы
    }
}