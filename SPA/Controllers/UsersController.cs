﻿using SPA.Models.DTO;
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
        private UsersService _usersService { get; set; }
        private IMongoCollection<BsonDocument> _collection { get; set; }
        public UsersController(IDbService dbService)
        {
            _usersService = new UsersService();
            _collection = dbService.db.GetCollection<BsonDocument>("Users");
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersData()
        {
            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dotNetObjList);
          
            var myObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDto>>(json);


            if (myObject.Count > 0)
                return Ok(myObject);
            return NotFound();
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
        public async Task<ActionResult<List<Object>>> GetAllUsersData()
        {

            var t = await _collection.Find(f => true).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);       

            if (dotNetObjList.Count > 0)
                return Ok(dotNetObjList);
            return NotFound();
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<Object>> GetAllUserData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var t = await _collection.Find(filter).ToListAsync();
            var dotNetObjList = t.ConvertAll(BsonTypeMapper.MapToDotNetValue);
         
            if (dotNetObjList.Count > 0)
                return Ok(dotNetObjList[0]);
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddNewUser([FromBody] User user)
        {

            _collection.InsertOneAsync(user.ToBsonDocument());
            return Ok();
        }

        [HttpPost("car")]
        public IActionResult AddCarToUser([FromBody] Car car)
        {
            _collection.InsertOneAsync(car.ToBsonDocument());
            return Ok();
        }


        public class UserFilter
         {
            public  int Timestamp { get; set; }
            public  int Machine { get; set; }
            public  short Pid { get; set; }
            public  int Increment { get; set; }
    
         }


    }
}
