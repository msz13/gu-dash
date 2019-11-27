using System;
using System.Collections.Generic;
using System.Text;

namespace GuDash.Common.MongoPersistance
{
    class MongoDbConfiguration
    {
        public string Url { get; private set; }

        public string Database { get; private set; }
    }
}
