using db.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static gui.Classes.IInformation;

namespace gui.Classes {
    public static class EFCoreConnect {
        private static readonly string[] typeName = new string[] { "Customers", "Drivers", "Orders", "Rates", "Routes", "TransportVehicles" };

        public static List<string>? GetTableNames<TContext>(TContext context) where TContext : DbContext {
            return context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Where(t => t != null && t != string.Empty)
                .Distinct()
                .ToList()!;
        }

        /// <summary>
        /// Получает IBindingList для указанной сущности по текстовому представлению типа.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="entityTypeName">Имя типа сущности (например, "Order").</param>
        /// <returns>IBindingList для указанной сущности.</returns>
        public static IBindingList? GetBindingListByEntityName(OrderDbContext context, string entityTypeName) {
            if (!typeName.Contains(entityTypeName))
                return null;
            switch (entityTypeName) {
                case "Customers":
                    context.Customers.OrderBy(c => c.Id).Load();
                    return context.Customers.Local.ToBindingList();
                case "Drivers":
                    context.Drivers.OrderBy(d => d.Id).Load();
                    return context.Drivers.Local.ToBindingList();
                case "Orders":
                    context.Orders.OrderBy(o => o.Id).Load();
                    return context.Orders.Local.ToBindingList();
                case "Rates":
                    context.Rates.OrderBy(r => r.Id).Load();
                    return context.Rates.Local.ToBindingList();
                case "Routes":
                    context.Routes.OrderBy(r => r.Id).Load();
                    return context.Routes.Local.ToBindingList();
                case "TransportVehicles":
                    context.TransportVehicles.OrderBy(r => r.Id).Load();
                    return context.TransportVehicles.Local.ToBindingList();
            }
            return null;
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
            var scalarResult = command.ExecuteScalar();
            if (scalarResult == null || scalarResult == DBNull.Value) { // user not found
                return false;
            }

            bool result = (int)scalarResult == 1;
            connection.Close();

            return result;
        }

        public static void ApplyChangesToDatabase(DbContext _context) {
            try {
                _context.SaveChanges();
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

        #region Deprecated

        /*public static IBindingList? GetBindingListByEntityType<TDbContext>(TDbContext context, Type entityType) where TDbContext : DbContext {
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
        }*/

        /*public static IBindingList? GetBindingListByEntityType<TDbContext>(TDbContext context, Type entityType) where TDbContext : DbContext {
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
        }*/

        #endregion
    }
}
