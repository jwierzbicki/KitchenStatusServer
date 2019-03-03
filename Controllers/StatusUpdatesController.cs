using KitchenStatusServer.Models;
using KitchenStatusServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenStatusServer.Controllers
{
    [Route("api/[controller]")]
    public class StatusUpdatesController : Controller
    {
        private readonly StatusUpdateService statusUpdateService;
        private readonly ProductsDbContext productsDbContext;

        public StatusUpdatesController(StatusUpdateService statusUpdateService)
        {
            this.statusUpdateService = statusUpdateService;
            //string connectionString = "Data Source=D:/CSharp/KitchenStatus/KitchenStatusServer/kitchen.db;";
            string connectionString = "Data Source=/home/pi/KitchenStatusServer/kitchen.db;";
            productsDbContext = ProductsDbContextFactory.Create(connectionString);
        }

        [HttpGet]
        public ActionResult<List<StatusUpdate>> Get()
        {
            return statusUpdateService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<StatusUpdate> Get(string id)
        {
            var statusUpdate = statusUpdateService.Get(id);

            if(statusUpdate == null)
            {
                return NotFound();
            }

            return statusUpdate;
        }

        [HttpPost]
        public ActionResult<StatusUpdate> Create([FromBody]StatusUpdate statusUpdate)
        {
            // Add status update to history in MongoDB
            statusUpdateService.Create(statusUpdate);

            foreach (var change in statusUpdate.Changes.ToList())
            {
                var product = productsDbContext.Products.First(x => x.Name == change.Name);
                product.StateCurrent -= change.Quantity;
            }

            productsDbContext.SaveChanges();
            

            return Created("api/[controller]", statusUpdate);
        }
    }
}
