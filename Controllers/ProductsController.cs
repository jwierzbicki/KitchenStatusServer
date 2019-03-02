using KitchenStatusServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenStatusServer.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController
    {
        private ProductsDbContext dbContext;

        public ProductsController()
        {
            string connectionString = "Data Source=D:/CSharp/KitchenStatus/KitchenStatusServer/kitchen.db;";
            dbContext = ProductsDbContextFactory.Create(connectionString);
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
