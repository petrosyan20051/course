using db.Contexts;
using db.Models;
using db.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Reflection;
using static gui.forms.BaseForm;

namespace gui.classes {

    public class Tools {

        public static List<string>? GetTableNames<TContext>(TContext context) where TContext : DbContext {
            return context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Where(t => t != null)
                .Distinct()
                .ToList();
        }

        public static BaseDbContext? CreateSelectedContext(string type, Dictionary<string, Type> contextTuple) {
            if (type.IsNullOrEmpty()) { // whether source type string is nullable or empty
                return null;
            }

            if (contextTuple.TryGetValue(type, out var contextType)) { // creates instance of context
                return Activator.CreateInstance(contextType) as BaseDbContext;
            }
            return null;
        }

        public static IList? DbSetFilterByRole(IList db, UserRights newRights, Type entityType) {
            if (db is null || entityType is null) {
                return null;
            }

            if (entityType == typeof(Order)) {
                return Filter(db as List<Order>, newRights);
            } else if (entityType == typeof(Customer)) {
                return Filter(db as List<Customer>, newRights);
            } else if (entityType == typeof(Driver)) {
                return Filter(db as List<Driver>, newRights);
            } else if (entityType == typeof(Route)) {
                return Filter(db as List<Route>, newRights);
            } else if (entityType == typeof(Rate)) {
                return Filter(db as List<Rate>, newRights);
            } else if (entityType == typeof(TransportVehicle)) {
                return Filter(db as List<TransportVehicle>, newRights);
            } else {
                return null;
            }
        }

        public static void HideColumnsFromDataGridView(DataGridView grid, string[] columnNames) {
            if (grid is null || columnNames is null || columnNames.IsNullOrEmpty()) {
                return;
            }

            foreach (var columnName in columnNames) {
                if (grid.Columns.Contains(columnName)) {
                    grid.Columns[columnName].Visible = false;
                }
            }
        }

        public static void ShowUpColumnsFromDataGridView(DataGridView grid, string[] columnNames) {
            if (grid is null || columnNames is null || columnNames.IsNullOrEmpty()) {
                return;
            }

            foreach (var columnName in columnNames) {
                if (grid.Columns.Contains(columnName)) {
                    grid.Columns[columnName].Visible = true;
                }
            }
        }

        private static IList? Filter<TEntity>(List<TEntity> db, UserRights newRights) where TEntity : BaseModel {
            if (newRights != UserRights.Admin) {
                return db?.Where(o => o.isDeleted == null).ToList();
            } else {
                return db?.ToList();
            }
        }

        public static IList? GetDbSet(OrderDbContext context, Type entityType) {
            if (context is null || entityType == null) {
                return null;
            }

            if (entityType == typeof(Order)) {
                return context.Orders.ToList();
            } else if (entityType == typeof(Customer)) {
                return context.Customers.ToList();
            } else if (entityType == typeof(Driver)) {
                return context.Drivers.ToList();
            } else if (entityType == typeof(Route)) {
                return context.Routes.ToList();
            } else if (entityType == typeof(Rate)) {
                return context.Rates.ToList();
            } else if (entityType == typeof(TransportVehicle)) {
                return context.TransportVehicles.ToList();
            } else {
                return null;
            }
        }

        // Reoder columns data of datagridview using type of entity
        public static void ReorderColumnsAccordingToDbContextByType(DataGridView grid, Type entityType) {
            if (grid is null || entityType == null) {
                return;
            }

            if (entityType == typeof(Order)) {
                ReorderColumnsAccordingToDbContext<Order>(grid);
            } else if (entityType == typeof(Customer)) {
                ReorderColumnsAccordingToDbContext<Customer>(grid);
            } else if (entityType == typeof(Driver)) {
                ReorderColumnsAccordingToDbContext<Driver>(grid);
            } else if (entityType == typeof(Route)) {
                ReorderColumnsAccordingToDbContext<Route>(grid);
            } else if (entityType == typeof(Rate)) {
                ReorderColumnsAccordingToDbContext<Rate>(grid);
            } else if (entityType == typeof(TransportVehicle)) {
                ReorderColumnsAccordingToDbContext<TransportVehicle>(grid);
            }
        }

        // Reorder columns names in order of declarinhg (see OrderDbContext)
        public static void ReorderColumnsAccordingToDbContext<TEntity>(DataGridView grid) where TEntity : class {
            var properties = typeof(TEntity).GetProperties()
                .OrderByDescending(p => p.GetCustomAttribute<DisplayPriorityAttribute>()?.IsHighPriority ?? true)
                .ThenBy(p => p.MetadataToken)
                .ToList();

            // Упорядочиваем столбцы в DataGridView
            foreach (var prop in properties) {
                if (grid.Columns.Contains(prop.Name)) {
                    grid.Columns[prop.Name].DisplayIndex = properties.IndexOf(prop);
                }
            }
        }
    }
}