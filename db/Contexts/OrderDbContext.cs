using db.Models;
using db.Tools;
using Microsoft.EntityFrameworkCore;

namespace db.Contexts {

    public class OrderDbContext : DbContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<TransportVehicle> TransportVehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Credential> Credentials { get; set; }

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

            int i = 1;
            modelBuilder.Entity<Customer>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Surname).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.PhoneNumber).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Email).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var customers = Generators.GenerateCustomers(500);
            modelBuilder.Entity<Customer>().HasData(customers);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<Driver>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Surname).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.PhoneNumber).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.DriverLicenceSeries).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.DriverLicenceNumber).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var drivers = Generators.GenerateDrivers(500);
            modelBuilder.Entity<Driver>().HasData(drivers);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<Models.Route>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.BoardingAddress).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.DropAddress).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var routes = Generators.GenerateRoutes(500);
            modelBuilder.Entity<Models.Route>().HasData(routes);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<TransportVehicle>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.DriverId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Number).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Series).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.RegistrationCode).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Model).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Color).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.ReleaseYear).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var vehicles = Generators.GenerateTransportVehicles(drivers, 500);
            modelBuilder.Entity<TransportVehicle>().HasData(vehicles);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<Rate>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.DriverId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.VehicleId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.MovePrice).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.IdlePrice).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var rates = Generators.GenerateRates(drivers, vehicles, 500);
            modelBuilder.Entity<Rate>().HasData(rates);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<Order>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.CustomerId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.RouteId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.RateId).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Distance).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            var orders = Generators.GenerateOrders(customers, routes, rates, 240);
            modelBuilder.Entity<Order>().HasData(orders);

            ///////////////////////////

            i = 1;
            modelBuilder.Entity<Credential>(entity => {
                entity.Property(e => e.Id).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Username).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Password).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Rights).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(i++);
                entity.Property(e => e.Note).HasColumnOrder(i++);
                entity.Property(e => e.WhoChanged).HasColumnOrder(i++);
                entity.Property(e => e.WhenChanged).HasColumnOrder(i++);
                entity.Property(e => e.isDeleted).HasColumnName("isDeleted").HasColumnOrder(i++);
            });

            modelBuilder.Entity<Credential>().HasData();
        }
    }
}