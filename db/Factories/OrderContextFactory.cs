using db.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace db.Factories {

    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderDbContext> {

        public OrderDbContext CreateDbContext(string[] args) {
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