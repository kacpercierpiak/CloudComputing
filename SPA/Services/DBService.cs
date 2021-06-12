using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Services
{
    public class DBService : IDbService
    {
        public IMongoDatabase db { get; private set; }

        public DBService()
        {
            var client = new MongoClient("mongodb+srv://root:zaq123456@cloudproject.9vnew.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");     
            db = client.GetDatabase("Cloud");
        }

    }
}
