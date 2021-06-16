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
    public class OrdersController : ControllerBase
    {
        private IMongoCollection<BsonDocument> _collection { get; set; }
        public OrdersController(IDbService dbService)
        {
            _collection = dbService.db.GetCollection<BsonDocument>("Orders");
        }

        [HttpGet()]
        public async Task<ActionResult<List<Object>>> GetAllUsersData()
        {

            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            if (dotNetObjList.Count > 0)
                return Ok(dotNetObjList);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetAllUserData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await _collection.Find(filter).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            if (dotNetObjList.Count > 0)
                return Ok(dotNetObjList[0]);
            return NotFound();
        }

        [HttpPost()]
        public IActionResult AddNewOrder([FromBody] Order order)
        {
            order.EndDate = null;
            order.StartDate = DateTime.UtcNow;
            order.OrderStatus = OrderStatus.New;
            
            _collection.InsertOneAsync(order.ToBsonDocument());
            return Ok();
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateUser([FromBody] Order order)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(order.OId));
            await _collection.ReplaceOneAsync(filter, order.ToBsonDocument());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            await _collection.DeleteOneAsync(filter);
            return Ok();
        }

    }
}
