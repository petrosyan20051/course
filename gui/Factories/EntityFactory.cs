using gui.Controllers;
using Microsoft.EntityFrameworkCore;

namespace gui.Factories {
    public static class EntityFactory {
        public static UserControl? CreateEntityFormByName(string name, DbContext context, string author) {
            return name switch {
                "Orders" => new OrderCreate(context, author),
                "Customers" => new CustomerCreate(context, author),
                _ => null
            };
        }
    }
}
