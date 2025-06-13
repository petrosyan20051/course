using db.Contexts;
using db.Custom_Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace db.Factories {

    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext> {

        // Create OrderDbContext using args:
        //  1. IP-address
        //  2. Database Name
        //  3. User login
        //  4. User password
        public OrderDbContext CreateDbContext(string[] args) {
            if (args.Length < 4) { // Must be 4 neccessary arguments
                return null;
            }
            
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionsBuilder.UseSqlServer(SqlConnect.GetConnectionString(args[0], args[1], args[2], args[3]));

            return new OrderDbContext(optionsBuilder.Options);
        }

        // Create OrderDbContext using default connectionString in appsettings.json
        public OrderDbContext CreateDefaultDbContext() {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // find appsetings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}