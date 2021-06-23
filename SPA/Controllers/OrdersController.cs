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
        public async Task<ActionResult<List<OrderDto>>> GetAllUsersData()
        {

            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);

            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderDto>>(json);

           
                return Ok(myObject);
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetAllUserData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await _collection.Find(filter).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);

            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderDto>>(json);

            if (myObject.Count() < 1)
                return NotFound();
                return Ok(myObject[0]);
           
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto order, string id)
        {
            if (order.OrderStatus == OrderStatus.Done)
                order.EndDate = DateTime.UtcNow;
            else
                order.EndDate = null;
           var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            await _collection.ReplaceOneAsync(filter, order.ToBsonDocument());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            await _collection.DeleteOneAsync(filter);
            return Ok();
        }

    }
}
