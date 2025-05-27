using db.Contexts;
using db.Factories;
using db.Models;
using gui.classes;
using System.Collections;

namespace gui.forms {

    public partial class TableInformation : BaseForm {

        public delegate void IsDeleteChangedHandler(MainForm.UserRights newRights, string currentDbSetName);

        public event IsDeleteChangedHandler IsDeleteChanged;

        private DataGridView _grid;
        private ComboBox _tblCmBox;

        //private Dictionary<string, Type> _contextTypes; // dict for flexible context to change from one to another
        private OrderDbContext _context; // db context

        public TableInformation(MainForm.UserRights userRights = MainForm.UserRights.Admin) {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;
            this.Rights = userRights;

            _tblCmBox.DataSource = Tools.GetTableNames<OrderDbContext>(_context);

            // Get source for datagridview
            try {
                var orders = Tools.GetFilteredDataByRole(_context.Orders, userRights); // get data about orders
                _grid.DataSource = orders; // bind data to grid
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
            _grid.DataSource = GetDbSourceForDataGridView((sender as ComboBox)?.Text)?.ToList();
        }

        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;

            this.IsDeleteChanged += OnIsDeletedChanged;

            // Make instance db context
            var factory = new OrderContextFactory();
            _context = factory.CreateDbContext([]);
        }

        private void OnIsDeletedChanged(MainForm.UserRights newRights, string currentDbSetName) {
            IsDeleteChanged?.Invoke(newRights, _tblCmBox.SelectedText);
        }

        private void HandleIsDeletedChanged(MainForm.UserRights user, string currentDbSetName) {
            //var source = GetDbSourceForDataGridView(currentDbSetName) as IQueryable;
            _grid.DataSource = Tools.GetFilteredDataByRole<BaseModel>(_grid.DataSource as IEnumerable<BaseModel>, user);
        }

        private IEnumerable<object>? GetDbSourceForDataGridView(string name) {
            Type? entityType = Type.GetType($"db.Models.{name}");

            var dbSetProperty = typeof(OrderDbContext).GetProperty(name);
            var dbSet = dbSetProperty?.GetValue(_context) as IEnumerable; // Приводим к IEnumerable
            return dbSet?.Cast<object>();
        }

        /*private IEnumerable<object>? GetFilteredDataByRole(IEnumerable<BaseModel> db, MainForm.UserRights newRights) {
            if (newRights != MainForm.UserRights.Admin) {
                return db.Where(o => o.isDeleted == null).ToList();
            } else {
                return db.Where(o => o.isDeleted != null).ToList();
            }
        }*/

        #endregion Пользовательские методы
    }
}