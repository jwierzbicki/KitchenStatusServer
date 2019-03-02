using Microsoft.EntityFrameworkCore;

namespace KitchenStatusServer.Models
{
    public class ProductsDbContextFactory
    {
        public static ProductsDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
            optionsBuilder.UseSqlite(connectionString);
            var productsDbContext = new ProductsDbContext(optionsBuilder.Options);
            return productsDbContext;
        }
    }
}
