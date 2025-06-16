using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace gui.Classes {
    public static class EFCoreConnect {
        public static List<string>? GetTableNames<TContext>(TContext context) where TContext : DbContext {
            return context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Where(t => t != null && t != string.Empty)
                .Distinct()
                .ToList()!;
        }

        /// <summary>
        /// Получает IBindingList для указанной сущности по имени типа.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="entityTypeName">Имя типа сущности (например, "Order").</param>
        /// <returns>IBindingList для указанной сущности.</returns>
        public static IBindingList? GetBindingListByEntityName(DbContext context, string entityTypeName) {
            // Find entity type using typeName
            var entityType = context.Model.FindEntityType(entityTypeName);
            

            // Get ClrType of entity
            var clrType = entityType?.ClrType;

            if (clrType == null) {
                return null;
            }
            // Get DbSet<entityType>
            var dbSet = typeof(DbContext).GetMethod("Set")?
                .MakeGenericMethod(clrType)?
                .Invoke(context, null);

            // Get local implementation of DbSet<T> and convert it into IBindingList
            var localProperty = dbSet?.GetType().GetProperty("Local");

            var localValue = localProperty?.GetValue(dbSet);

            var toBindingListMethod = localValue.GetType().GetMethod("ToBindingList");

            return (IBindingList)toBindingListMethod?.Invoke(localValue, null);
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

        /*public static IBindingList? GetDbSet<TDbContext>(TDbContext context, Type entityType) where TDbContext : DbContext {
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
    }
}
