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
    public class UsersController : ControllerBase
    {       
        private IMongoCollection<BsonDocument> _collection { get; set; }
        public UsersController(IDbService dbService)
        {        
            _collection = dbService.db.GetCollection<BsonDocument>("Users");
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersData()
        {
            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);
          
            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDto>>(json);


         
                return Ok(myObject);
          
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await _collection.Find(filter).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);

            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDto>>(json);

            if(myObject.Count>0)
            return Ok(myObject[0]);
            return NotFound();
        }
        [HttpGet("all/details")]
        public async Task<ActionResult<IEnumerable<UserCarDto>>> GetAllUsersData()
        {

            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);

            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserCarDto>>(json);
           
                return Ok(myObject);
           
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<UserCarDto>> GetAllUserData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await _collection.Find(filter).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);

            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserCarDto>>(json);

            if (myObject.Count > 0)
                return Ok(myObject[0]);
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddNewUser([FromBody] User user)
        {

            _collection.InsertOneAsync(user.ToBsonDocument());
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {           
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(user.OId));
            var update = Builders<BsonDocument>.Update.Set("FirstName", user.FirstName).Set("LastName", user.LastName).Set("Phone", user.Phone);
            await _collection.UpdateOneAsync(filter, update);
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
