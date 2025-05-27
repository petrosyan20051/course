using db.Contexts;
using db.Models;
using gui.forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    }
}