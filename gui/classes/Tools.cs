using db.Contexts;
using Microsoft.EntityFrameworkCore;

namespace gui.classes {

    public class Tools {

        public static List<string> GetTableNames<TContext>(TContext context) where TContext : DbContext {
            return context.Model.GetEntityTypes()
                .Select(e => e.GetTableName())
                .Where(t => t != null)
                .Distinct()
                .ToList();
        }
    }
}