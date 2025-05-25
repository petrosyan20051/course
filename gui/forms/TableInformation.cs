using db.Contexts;
using db.Factories;
using gui.classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;

namespace gui.forms {

    public partial class TableInformation : Form {
        private DataGridView _grid;
        private ComboBox _tblCmBox;

        //private Dictionary<string, Type> _contextTypes; // dict for flexible context to change from one to another
        private OrderDbContext _context; // db context

        public TableInformation() {
            InitializeComponent();
            InitVariables();
            this.TopLevel = false;

            //_tblCmBox.DataSource = _contextTypes.Keys.ToList(); // bind sources for combobox

            // Make instance db context
            var factory = new OrderContextFactory();
            _context = factory.CreateDbContext([]);

            // Get source for datagridview
            try {
                var orders = _context?.Orders.ToList(); // get data about orders
                _grid.DataSource = orders; // bind data to grid
            } catch (Exception ex) {
                MessageBox.Show($"Произошла ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _grid.AutoResizeColumns(); // resize columns
            _tblCmBox.DataSource = Tools.GetTableNames<OrderDbContext>(_context);
        }

        private void TableInformation_Load(object sender, EventArgs e) {
            _grid.BackgroundColor = Design.DataGridViewDarkThemeColor;
        }

        private void tableLst_SelectedIndexChanged(object sender, EventArgs e) {
            string selectedTypeName = (sender as ComboBox).Text;
            Type entityType = Type.GetType($"db.Models.{selectedTypeName}");

            var dbSetProperty = typeof(OrderDbContext).GetProperty((sender as ComboBox).Text);
            var dbSet = dbSetProperty.GetValue(_context) as IEnumerable; // Приводим к IEnumerable
            _grid.DataSource = dbSet?.Cast<object>().ToList();
        }

        #region Пользовательские методы

        private void InitVariables() {
            _grid = this.dbGrid;
            _tblCmBox = this.tableLst;

            /*// Initialize dict
            _contextTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(BaseDbContext).IsAssignableFrom(t) && !t.IsAbstract)
                .ToDictionary(t => t.Name, t => t);*/
        }

        #endregion Пользовательские методы
    }
}