using KitchenStatusServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace KitchenStatusServer.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController
    {
        private ProductsDbContext dbContext;

        public ProductsController(ProductsDbContext productsDbContext)
        {
            dbContext = productsDbContext;
        }

        [HttpGet]
        public Product[] GetProducts()
        {
            return dbContext.Products.ToArray();
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            var target = dbContext.Products.SingleOrDefault(p => p.Id == id);
            return target;
        }
    }
}
