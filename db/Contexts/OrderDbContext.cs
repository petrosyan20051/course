﻿using db.Models;
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
        public DbSet<Role> Roles { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) {
            Database.EnsureCreated();
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
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd().HasColumnOrder(1);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.Surname).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.PhoneNumber).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.Email).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.Note).HasColumnOrder(8);
                entity.Property(e => e.WhoChanged).HasColumnOrder(9);
                entity.Property(e => e.WhenChanged).HasColumnOrder(10);
                entity.Property(e => e.IsDeleted).HasColumnOrder(11);
            });

            var customers = Generators.GenerateCustomers(500);
            modelBuilder.Entity<Customer>().HasData(customers);

            ///////////////////////////

            modelBuilder.Entity<Driver>(entity => {
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd().HasColumnOrder(1);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.Surname).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.PhoneNumber).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.DriverLicenceSeries).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.DriverLicenceNumber).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(8);
                entity.Property(e => e.Note).HasColumnOrder(9);
                entity.Property(e => e.WhoChanged).HasColumnOrder(10);
                entity.Property(e => e.WhenChanged).HasColumnOrder(11);
                entity.Property(e => e.IsDeleted).HasColumnOrder(12);
            });

            var drivers = Generators.GenerateDrivers(500);
            modelBuilder.Entity<Driver>().HasData(drivers);

            ///////////////////////////

            modelBuilder.Entity<Models.Route>(entity => {
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd().HasColumnOrder(1);
                entity.Property(e => e.BoardingAddress).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.DropAddress).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.Note).HasColumnOrder(6);
                entity.Property(e => e.WhoChanged).HasColumnOrder(7);
                entity.Property(e => e.WhenChanged).HasColumnOrder(8);
                entity.Property(e => e.IsDeleted).HasColumnOrder(9);
            });

            var routes = Generators.GenerateRoutes(500);
            modelBuilder.Entity<Models.Route>().HasData(routes);

            ///////////////////////////

            modelBuilder.Entity<TransportVehicle>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().HasColumnOrder(1);
                entity.Property(e => e.DriverId).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.Number).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.Series).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.RegistrationCode).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.Model).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.Color).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.ReleaseYear).IsRequired().HasColumnOrder(8);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(9);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(10);
                entity.Property(e => e.Note).HasColumnOrder(11);
                entity.Property(e => e.WhoChanged).HasColumnOrder(12);
                entity.Property(e => e.WhenChanged).HasColumnOrder(13);
                entity.Property(e => e.IsDeleted).HasColumnOrder(14);
            });

            var vehicles = Generators.GenerateTransportVehicles(drivers, 500);
            modelBuilder.Entity<TransportVehicle>().HasData(vehicles);

            ///////////////////////////

            modelBuilder.Entity<Rate>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().HasColumnOrder(1);
                entity.Property(e => e.Forename).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.DriverId).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.VehicleId).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.MovePrice).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.IdlePrice).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(8);
                entity.Property(e => e.Note).HasColumnOrder(9);
                entity.Property(e => e.WhoChanged).HasColumnOrder(10);
                entity.Property(e => e.WhenChanged).HasColumnOrder(11);
                entity.Property(e => e.IsDeleted).HasColumnOrder(12);
            });

            var rates = Generators.GenerateRates(drivers, vehicles, 500);
            modelBuilder.Entity<Rate>().HasData(rates);

            ///////////////////////////

            modelBuilder.Entity<Order>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().HasColumnOrder(1);
                entity.Property(e => e.CustomerId).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.RouteId).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.RateId).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.Distance).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.Note).HasColumnOrder(8);
                entity.Property(e => e.WhoChanged).HasColumnOrder(9);
                entity.Property(e => e.WhenChanged).HasColumnOrder(10);
                entity.Property(e => e.IsDeleted).HasColumnOrder(11);
            });

            var orders = Generators.GenerateOrders(customers, routes, rates, 240);
            modelBuilder.Entity<Order>().HasData(orders);

            ///////////////////////////

            modelBuilder.Entity<Credential>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().HasColumnOrder(1);
                entity.Property(e => e.RoleId).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.Username).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.Password).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.Note).HasColumnOrder(7);
                entity.Property(e => e.WhoChanged).HasColumnOrder(8);
                entity.Property(e => e.WhenChanged).HasColumnOrder(9);
                entity.Property(e => e.IsDeleted).HasColumnOrder(10);
            });

            modelBuilder.Entity<Credential>().HasData();

            ///////////////////////////

            modelBuilder.Entity<Role>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired().HasColumnOrder(1);
                entity.Property(e => e.Rights).IsRequired().HasColumnOrder(2);
                entity.Property(e => e.CanGet).IsRequired().HasColumnOrder(3);
                entity.Property(e => e.CanPost).IsRequired().HasColumnOrder(4);
                entity.Property(e => e.CanUpdate).IsRequired().HasColumnOrder(5);
                entity.Property(e => e.CanDelete).IsRequired().HasColumnOrder(6);
                entity.Property(e => e.WhoAdded).IsRequired().HasColumnOrder(7);
                entity.Property(e => e.WhenAdded).IsRequired().HasColumnOrder(8);
                entity.Property(e => e.WhoChanged).HasColumnOrder(9);
                entity.Property(e => e.WhenChanged).HasColumnOrder(10);
                entity.Property(e => e.IsDeleted).HasColumnOrder(11);
            });

            modelBuilder.Entity<Role>().HasData();
        }
    }
}