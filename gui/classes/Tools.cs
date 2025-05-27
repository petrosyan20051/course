using db.Models;
using gui.forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public static IEnumerable<object>? GetFilteredDataByRole<TEntity>(IEnumerable<TEntity> db, MainForm.UserRights newRights) where TEntity : BaseModel {
            if (newRights != MainForm.UserRights.Admin) {
                return db?.Where(o => o.isDeleted == null).ToList();
            } else {
                return db?.ToList();
            }
        }
    }
}