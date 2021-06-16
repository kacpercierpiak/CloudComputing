using SPA.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private IMongoCollection<BsonDocument> _collection { get; set; }
        public CarsController(IDbService dbService)
        {
            _collection = dbService.db.GetCollection<BsonDocument>("Users");
        }

        [HttpPost("{id}/car")]
        public async Task<IActionResult> AddNewCar([FromBody] Car car, string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t.Cars is null)
                t.Cars = new List<Car>();
            t.Cars.Add(car);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }
        [HttpPost("{id}/car/{numberPlate}/history")]
        public async Task<IActionResult> AddCarHistory([FromBody] CarHistory carHistory, string id, string numberPlate)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            var index = t.Cars.FindIndex(x => x.NumberPlate == numberPlate);

            if (t.Cars[index].CarHistories is null)
                t.Cars[index].CarHistories = new List<CarHistory>();

            t.Cars[index].CarHistories.Add(carHistory);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }

        [HttpPut("{id}/car/{numberPlate}")]
        public async Task<IActionResult> UpdateCar([FromBody] Car car, string id, string numberPlate)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            t.Cars[t.Cars.FindIndex(x => x.NumberPlate == numberPlate)] = car;
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }

        [HttpDelete("{id}/car/{numberPlate}")]
        public async Task<IActionResult> DeleteCar(string id, string numberPlate)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            var c = t.Cars.FirstOrDefault(x => x.NumberPlate == numberPlate);
            t.Cars.Remove(c);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }
    }
}
