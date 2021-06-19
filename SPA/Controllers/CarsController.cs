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
using AutoMapper;

namespace SPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private IMongoCollection<BsonDocument> _collection { get; set; }
        private readonly IMapper _mapper;
        public CarsController(IDbService dbService, IMapper mapper)
        {
            _collection = dbService.db.GetCollection<BsonDocument>("Users");
            _mapper = mapper;
        }

        [HttpPost("{id}/car")]
        public async Task<IActionResult> AddNewCar([FromBody] CarDto carDto, string id)
        {
            Car car = _mapper.Map<Car>(carDto);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            car._id = ObjectId.GenerateNewId();
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t.Cars is null)
                t.Cars = new List<Car>();
            t.Cars.Add(car);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }
        [HttpPost("{id}/car/{carId}/history")]
        public async Task<IActionResult> AddCarHistory([FromBody] CarHistory carHistory, string id, string carId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            var index = t.Cars.FindIndex(x => x.OId == carId);

            if (t.Cars[index].CarHistories is null)
                t.Cars[index].CarHistories = new List<CarHistory>();

            t.Cars[index].CarHistories.Add(carHistory);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }
        [HttpGet("{id}/car/{carId}")]
        public async Task<ActionResult<Car>> GetCar(string id, string carId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            
          
            return Ok(t.Cars[t.Cars.FindIndex(x => x.OId == carId)]);
        }

        [HttpPut("{id}/car/{carId}")]
        public async Task<IActionResult> UpdateCar([FromBody] Car car, string id, string carId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            var oldCar = t.Cars[t.Cars.FindIndex(x => x.OId == carId)];
            car.OId = oldCar.OId;
            car._id = oldCar._id;
            car.CarHistories = oldCar.CarHistories;
            t.Cars[t.Cars.FindIndex(x => x.OId == carId)] = car;
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }

        [HttpDelete("{id}/car/{carId}")]
        public async Task<IActionResult> DeleteCar(string id, string carId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await (await _collection.FindAsync<UserCar>(filter).ConfigureAwait(false)).FirstOrDefaultAsync();
            if (t is null || t.Cars is null)
                return NotFound();
            var c = t.Cars.FirstOrDefault(x => x.OId == carId);
            t.Cars.Remove(c);
            await _collection.ReplaceOneAsync(filter, t.ToBsonDocument());
            return Ok();
        }
    }
}
