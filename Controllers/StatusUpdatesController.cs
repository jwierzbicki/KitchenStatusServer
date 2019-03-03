using KitchenStatusServer.Models;
using KitchenStatusServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KitchenStatusServer.Controllers
{
    [Route("api/[controller]")]
    public class StatusUpdatesController : Controller
    {
        private readonly StatusUpdateService statusUpdateService;
        private readonly ProductsDbContext productsDbContext;

        public StatusUpdatesController(ProductsDbContext productsDbContext, StatusUpdateService statusUpdateService)
        {
            this.statusUpdateService = statusUpdateService;
            this.productsDbContext = productsDbContext;
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
            // Add status update to history in LiteDB
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
