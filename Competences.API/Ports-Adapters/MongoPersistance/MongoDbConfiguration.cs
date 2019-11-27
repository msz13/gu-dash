using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.API.Ports_Adapters.MongoPersistance
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
       
        public string Url { get;  set; }

        public string DatabaseName { get;  set; }
    }

    interface IMongoDbConfiguration
    {
        string Url { get; set; }

        string DatabaseName { get;  set; }

    }

}
