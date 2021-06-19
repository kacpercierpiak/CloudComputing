using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Services
{
    public interface IDbService
    {
        public IMongoDatabase db { get;}
    }
}
