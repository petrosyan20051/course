using Microsoft.EntityFrameworkCore;

namespace db.Models {

    public class ApplicationContext : DbContext {
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KR;Trusted_Connection=True;");
            }
        }
    }
}