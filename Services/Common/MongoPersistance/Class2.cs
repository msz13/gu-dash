namespace GuDash.Common.MongoPersistance
{
    class MongoDbContext
    {
        public string Client { get; private set; } //add db type from mongo

        public string Db { get; private set; } //add db type from mongo

        public string GetCollection(string collectionName) { return "collection"; }

    }
}
