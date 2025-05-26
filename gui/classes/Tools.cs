using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using gui.forms;

namespace gui.classes {

    public class Tools {

        public static List<string> GetTableNames<TContext>(TContext context) where TContext : DbContext {
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

        public static List<TEntity>? GetFilteredDataByRole<TEntity, TProperty>(DbSet<TEntity> dbSet, MainForm.UserRights newRights)
            where TEntity : class {
            var query = dbSet.AsQueryable();

            if (newRights != MainForm.UserRights.Admin) {
                var hasIsDeleted = typeof(TEntity).GetProperty("isDeleted") != null; // has TEntity property "isDeleted"?
                if (hasIsDeleted) {
                    return query.Where(e => EF.Property<TProperty?>(e, "isDeleted") == null)?.ToList(); //
                }
            } else {
                return query?.ToList();
            }

            return null;
        }
    }
}