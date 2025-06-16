using db.Models;
using db.Repositories;
using db.Tools;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using static gui.Classes.IInformation;

namespace gui.Classes {

    public class Tools {

        public static List<string>? GetTableNames<TContext>(TContext context) where TContext : DbContext {
            return context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Where(t => t != null)
                .Distinct()
                .ToList();

            context.Set<Customer>().Local.ToBindingList();
        }

        public static IList? DbSetFilterByRole(IList db, UserRights newRights, Type entityType) {
            if (db is null || entityType is null) {
                return null;
            }

            // Make generic methods using reflection
            var filterGenericMethod = typeof(Tools).GetMethod("Filter", BindingFlags.Static | BindingFlags.NonPublic)?.MakeGenericMethod(entityType);
            var castGenericMathod = typeof(Enumerable)?.GetMethod("Cast", BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(entityType);
            var toListGenericMethod = typeof(Enumerable).GetMethod("ToList", BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(entityType);

            var castedDb = castGenericMathod?.Invoke(null, new object[] { db }); // casted: IList db -> List<entityType>
            var castedDbToList = toListGenericMethod?.Invoke(null, new object[] { castedDb }); // db.ToList()
            return (IList)filterGenericMethod?.Invoke(null, new object[] { castedDbToList, newRights }); // FilterByRoles using    
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

        public static IBindingList? GetDbSet<TDbContext>(TDbContext context, Type entityType) where TDbContext : DbContext {
            if (context is null || entityType is null) {
                return null;
            }

            // Get DbSet<entityType> 
            var dbSetMethod = typeof(TDbContext)?.GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(entityType);
            dynamic? dbSet = dbSetMethod?.Invoke(context, null); // DbSet<>

            // Make request and get List<entityType>
            var toListGenericMethod = typeof(Enumerable).GetMethod("ToList", BindingFlags.Static | BindingFlags.Public)?.MakeGenericMethod(entityType);
            dynamic? list = toListGenericMethod?.Invoke(null, new object[] { dbSet });

            // Make BindingList<User>
            var bindingListType = typeof(BindingList<>).MakeGenericType(entityType);
            var bindingList = (IBindingList)Activator.CreateInstance(bindingListType, new object[] { list });

            return bindingList;
        }

        // Reoder columns data of datagridview using type of entity
        public static void ReorderColumnsAccordingToDbContextByType(DataGridView grid, Type entityType) {
            if (grid is null || entityType is null) {
                return;
            }

            // Make genetic type for ReorderColumnsAccordingToDbContext<entityType>
            var reorderGenericMethod = typeof(Tools)
                .GetMethod("ReorderColumnsAccordingToDbContext", BindingFlags.Static | BindingFlags.Public)?
                .MakeGenericMethod(entityType);
            reorderGenericMethod?.Invoke(null, new object[] { grid });
        }

        // Reorder columns names in order of declarinhg (see OrderDbContext)
        public static void ReorderColumnsAccordingToDbContext<TEntity>(DataGridView grid) where TEntity : class {
            var properties = typeof(TEntity).GetProperties()
                .OrderByDescending(p => p.GetCustomAttribute<DisplayPriorityAttribute>()?.IsHighPriority ?? true)
                .ThenBy(p => p.MetadataToken)
                .ToList();

            // Sort Columns in DataGridView
            foreach (var prop in properties) {
                if (grid.Columns.Contains(prop.Name)) {
                    grid.Columns[prop.Name].DisplayIndex = properties.IndexOf(prop);
                }
            }
        }

        public static bool CheckSqlServerPermissionsForAdmin<TDbContext>(TDbContext context, string userName) where TDbContext : DbContext {
            if (userName == string.Empty) {
                return true;
            }
            
            using var connection = new SqlConnection(context.Database.GetConnectionString());
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @$"
                SELECT 
                    IS_ROLEMEMBER('db_datawriter', p.name) AS can_write
                FROM sys.database_principals p
                WHERE p.type_desc = 'SQL_USER' 
                    AND p.is_fixed_role = 0
                    AND p.name = '{userName}';";
            bool result = (int)command.ExecuteScalar() == 1;
            connection.Close();

            return result;         
        }



        #region Deprecated

        // Method to convert "input" to type "targetType"
        public static object? TryConvert(object input, Type targetType) {
            var converter = TypeDescriptor.GetConverter(targetType);
            try {
                return converter.ConvertFrom(input);
            } catch {
                throw new ArgumentException($"Converting object cannot be casted" +
                    $"to type \"{targetType.Name}\"");
            }

        }

        public static dynamic? GetRepositoryByName<TDbContext>(TDbContext context, Type entityType) where TDbContext : DbContext {
            // Finding Id property to determine TKey
            var keyType = entityType?.GetProperty("Id")?.PropertyType;

            // Now get repos
            var repositoryType = Type.GetType($"db.Repositories.{entityType?.Name}Repository, db");

            // Check where it implements IRepository<TEntity, TKey>
            var genericRepoType = typeof(IRepository<,>).MakeGenericType(entityType, keyType);

            //Make instance of repos with giving context
            var constructor = repositoryType?.GetConstructor(new[] { context.GetType() });

            var repositoryInstance = constructor?.Invoke(new object[] { context }); // make repository

            return repositoryInstance;
        }





        #endregion


    }
}