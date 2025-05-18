using Microsoft.EntityFrameworkCore;

namespace db.Models {

    public class ApplicationContext : DbContext {
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Server=16.0.1135.2;Database=KR;Trusted_Connection=True;");
            }
        }
    }
}