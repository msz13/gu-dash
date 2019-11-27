using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GuDash.Competences.API.Ports_Adapters.MongoPersistance
{
    public class MongoDbContext
    {
        public IMongoClient Client { get; private set; } //add db type from mongo

        public IMongoDatabase Db { get; private set; } //add db type from mongo

        public MongoDbContext(string url, string dbName)
        {
            Client = new MongoClient(url);
            Db = Client.GetDatabase(dbName);
        }




        public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
        {

            return Db.GetCollection<TDocument>(collectionName);
        }
    }
}
