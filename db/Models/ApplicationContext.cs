using Microsoft.EntityFrameworkCore;

namespace db.Models {

    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options) {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Rate> Rates => Set<Rate>();
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<TransportVehicle> Vehicles => Set<TransportVehicle>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Customer> Customers => Set<Customer>();

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=KR;Trusted_Connection=True");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}