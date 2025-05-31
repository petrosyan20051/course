using db.Contexts;
using db.Factories;
using db.Models;
using gui.classes;
using Microsoft.EntityFrameworkCore;
using System.Collections;

using TypeId = int;

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

        private string? currentCell;
        private bool isUpdatingCellValue = false;


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
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dynamic? repository = Tools.GetRepositoryByName(_context, tableMapping[_tblCmBox.Text]);

            // Get DbSet<entityType> 
            var dbSetMethod = typeof(OrderDbContext)?.GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(tableMapping[_tblCmBox.Text]);
            dynamic? dbSet = dbSetMethod?.Invoke(_context, null); // DbSet<>

            // Get entity constructor and its instance
            var entityConstructor = tableMapping[_tblCmBox.Text].GetConstructor(new Type[] { });
            dynamic? entityInstance = entityConstructor?.Invoke(new object[] { });

            // Set Id to instance
            entityInstance.Id = await repository?.NewIdToAdd();

            // Make instance for new object set 
            await dbSet?.AddAsync(entityInstance);

            await _context.SaveChangesAsync();

            _grid.DataSource = Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]);

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

        private async void dbGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (isUpdatingCellValue) {
                return;
            }

            try {
                isUpdatingCellValue = true;

                using (var _context = new OrderContextFactory().CreateDbContext([])) {
                    dynamic? repository = Tools.GetRepositoryByName(_context, tableMapping[_tblCmBox.Text]);

                    // Get DbSet<entityType> 
                    var dbSetMethod = typeof(OrderDbContext)?.GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(tableMapping[_tblCmBox.Text]);
                    dynamic? dbSet = dbSetMethod?.Invoke(_context, null); // DbSet<>

                    if ((sender as DataGridView)?.Columns[e.ColumnIndex].Name == "Id") { // Column "Id"
                        TypeId value = TypeId.Parse((sender as DataGridView)?
                            .Rows[e.RowIndex]?
                            .Cells[e.ColumnIndex]?
                            .Value?
                            .ToString());
                        var entityExists = await repository?.GetByIdAsync(value);
                        if (entityExists != null) {
                            MessageBox.Show(
                                $"Сущность с id = {value} уже существует.",
                                AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            (sender as DataGridView)?.Rows[e.RowIndex]?.Cells[e.ColumnIndex]?.Value = currentCell;
                            return;
                        }
                    }
                    ApplyChangesToDatabase(_context);

                    _grid.DataSource = Tools.GetDbSet(this._context, tableMapping[_tblCmBox.Text]);
                }
            } finally {
                isUpdatingCellValue = false;
            }
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

            _grid.Invalidate();
        }

        private async void ApplyChangesToDatabase(OrderDbContext _context) {
            try {
                if (_context.ChangeTracker.HasChanges()) {
                    await _context.SaveChangesAsync();

                    // Updata Data Source
                    _grid.DataSource = Tools.GetDbSet(_context, tableMapping[_tblCmBox.Text]);
                }
            } catch (DbUpdateException dbEx) {
                MessageBox.Show($"Ошибка при обновлении базы данных: {dbEx.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Пользовательские методы


    }
}