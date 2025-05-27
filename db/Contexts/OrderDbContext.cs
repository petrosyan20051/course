using db.Models;
using db.Tools;
using Microsoft.EntityFrameworkCore;

namespace db.Contexts {

    public class OrderDbContext : BaseDbContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=192.168.0.104;Database=KR;User ID=remote_user;Password=JcGDN9ST5KEG!;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            var customers = Generators.GenerateCustomers(500);
            var routes = Generators.GenerateRoutes(500);
            var drivers = Generators.GenerateDrivers(500);
            var vehicles = Generators.GenerateTransportVehicles(drivers, 500);
            var rates = Generators.GenerateRates(drivers, vehicles, 500);
            var orders = Generators.GenerateOrders(customers, routes, rates, 240);

            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Models.Route>().HasData(routes);
            modelBuilder.Entity<Driver>().HasData(drivers);
            modelBuilder.Entity<TransportVehicle>().HasData(vehicles);
            modelBuilder.Entity<Rate>().HasData(rates);
            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}