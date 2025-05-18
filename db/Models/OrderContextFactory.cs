using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace db.Models {

    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext> {

        public OrderContext CreateDbContext(string[] args) {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // find appsetings.json
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new OrderContext(optionsBuilder.Options);
        }
    }
}