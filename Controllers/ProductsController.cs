using KitchenStatusServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("shoppinglist")]
        public ProductToBuy[] GenerateShoppingList()
        {
            List<Product> allProducts = dbContext.Products.ToList();
            List<ProductToBuy> shoppingList = new List<ProductToBuy>();

            foreach (var product in allProducts)
            {
                if(product.StateCurrent < product.StateMinimal)
                {
                    var amount = product.StateCurrent;
                    var tobuy = 0.0;

                    while(amount < product.StateMinimal)
                    {
                        amount += product.UnitQuantity;
                        tobuy++;
                    }
                    tobuy *= product.UnitQuantity;
                    
                    shoppingList.Add(new ProductToBuy() { Name = product.Name, Amount = tobuy });
                }
            }

            return shoppingList.ToArray();
        }
    }
}
