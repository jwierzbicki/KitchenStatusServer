using KitchenStatusServer.Models;
using LiteDB;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace KitchenStatusServer.Services
{
    public class StatusUpdateService
    {
        static LiteCollection<StatusUpdate> statusUpdates;

        public StatusUpdateService()
        {
            var db = new LiteDatabase(@"KitchenHistory.db");
            statusUpdates = db.GetCollection<StatusUpdate>("KitchenHistory");
        }

        public List<StatusUpdate> Get()
        {
            return statusUpdates.Find(s => true).ToList();
        }

        public StatusUpdate Get(string id)
        {
            return statusUpdates.Find(x => x.Id.ToString() == id).SingleOrDefault();
        }

        public StatusUpdate Create(StatusUpdate statusUpdate)
        {
            statusUpdates.Insert(statusUpdate); // id will auto increment
            return statusUpdate;
        }
    }
}
