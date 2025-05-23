using Microsoft.EntityFrameworkCore;
using db.Tools;

namespace db.Models {

    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options) {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TransportVehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=Laptop;Database=KR;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                Generators.GenerateCustomers(200));
            modelBuilder.Entity<Route>().HasData(
                new Route { Id = 1, BoardingAddress = "Moscow", DropAddress = "Istra" });
            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, Forename = "Max", Surname = "Morozov", PhoneNumber = "+7853058034", DriverLicenceSeries = "23032", DriverLicenceNumber = "3280423" });
            modelBuilder.Entity<TransportVehicle>().HasData(
                new TransportVehicle { Id = 1, DriverId = 1, Number = "34111", Series = "324242", RegistrationCode = 4234, Model = "Toyota Land Cruiser", Color = "Gray", ReleaseYear = "2012" });
            modelBuilder.Entity<Rate>().HasData(
                new Rate { Id = 1, Forename = "Basic", DriverId = 1, VehicleId = 1, MovePrice = 100, IdlePrice = 50 });
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, RouteId = 1, RateId = 1, Distance = 100 });               
        }
    }
}