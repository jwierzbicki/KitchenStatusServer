using KitchenStatusServer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace KitchenStatusServer.Services
{
    public class StatusUpdateService
    {
        private readonly IMongoCollection<StatusUpdate> statusUpdates;

        public StatusUpdateService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("KitchenHistoryDb"));
            var database = client.GetDatabase("KitchenHistoryDb");
            statusUpdates = database.GetCollection<StatusUpdate>("KitchenHistory");
        }

        public List<StatusUpdate> Get()
        {
            return statusUpdates.Find(s => true).ToList();
        }

        public StatusUpdate Get(string id)
        {
            return statusUpdates.Find<StatusUpdate>(stat => stat.Id == id).FirstOrDefault();
        }

        public StatusUpdate Create(StatusUpdate statusUpdate)
        {
            statusUpdates.InsertOne(statusUpdate);
            return statusUpdate;
        }
    }
}
