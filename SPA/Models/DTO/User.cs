using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SPA.Models.DTO
{
    public class User : UserDto
    {
        [BsonId]
        
        public ObjectId _id { get; set; }
        public new string OId { get => _id.ToString(); }


    }

    public class UserDto
    {
        [JsonProperty("_id")]
        public string OId{ get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



    }




}
