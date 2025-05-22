using Microsoft.EntityFrameworkCore;

namespace db.Models {

    public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options) {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TransportVehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=PC;Database=KR;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().Property(o => o.Id).HasColumnName("Id_order");
            modelBuilder.Entity<Order>().Property(o => o.CustomerId).HasColumnName("Id_customer");
            modelBuilder.Entity<Order>().Property(o => o.RateId).HasColumnName("Id_rate");
            modelBuilder.Entity<Order>().Property(o => o.RouteId).HasColumnName("Id_route");
            modelBuilder.Entity<Order>().Property(o => o.Distance).HasColumnName("Distance");
            modelBuilder.Entity<Order>().Property(o => o.WhoAdded).HasColumnName("Who_Added");
            modelBuilder.Entity<Order>().Property(o => o.WhenAdded).HasColumnName("When_Added");
            modelBuilder.Entity<Order>().Property(o => o.WhoChanged).HasColumnName("Who_Changed");
            modelBuilder.Entity<Order>().Property(o => o.WhenChanged).HasColumnName("When_Changed");
        }
    }
}