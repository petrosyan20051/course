using db.Models;
using db.Tools;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace db.Contexts {

    public class OrderDbContext : BaseDbContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build(); // set appsettings for connectionStrings
            //optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

           // optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=KR;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer("Server=192.168.1.140;Database=KR;User ID=remote_user;Password=JcGDN9ST5KEG!;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Forename).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var customers = Generators.GenerateCustomers(500);
            modelBuilder.Entity<Customer>().HasData(customers);

            ///////////////////////////

            modelBuilder.Entity<Driver>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Forename).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.DriverLicenceSeries).IsRequired();
                entity.Property(e => e.DriverLicenceNumber).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var drivers = Generators.GenerateDrivers(500);
            modelBuilder.Entity<Driver>().HasData(drivers);

            ///////////////////////////

            modelBuilder.Entity<Models.Route>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.BoardingAddress).IsRequired();
                entity.Property(e => e.DropAddress).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var routes = Generators.GenerateRoutes(500);
            modelBuilder.Entity<Models.Route>().HasData(routes);

            ///////////////////////////

            modelBuilder.Entity<TransportVehicle>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.DriverId).IsRequired();
                entity.Property(e => e.Number).IsRequired();
                entity.Property(e => e.Series).IsRequired();
                entity.Property(e => e.Model).IsRequired();
                entity.Property(e => e.Color).IsRequired();
                entity.Property(e => e.ReleaseYear).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var vehicles = Generators.GenerateTransportVehicles(drivers, 500);
            modelBuilder.Entity<TransportVehicle>().HasData(vehicles);

            ///////////////////////////

            modelBuilder.Entity<Rate>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Forename).IsRequired();
                entity.Property(e => e.DriverId).IsRequired();
                entity.Property(e => e.VehicleId).IsRequired();
                entity.Property(e => e.MovePrice).IsRequired();
                entity.Property(e => e.IdlePrice).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var rates = Generators.GenerateRates(drivers, vehicles, 500);
            modelBuilder.Entity<Rate>().HasData(rates);

            modelBuilder.Entity<Order>(entity => {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.RouteId).IsRequired();
                entity.Property(e => e.RateId).IsRequired();
                entity.Property(e => e.Distance).IsRequired();
                entity.Property(e => e.WhoAdded).IsRequired();
                entity.Property(e => e.WhenAdded).IsRequired();
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted");
            });

            var orders = Generators.GenerateOrders(customers, routes, rates, 240);
            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}