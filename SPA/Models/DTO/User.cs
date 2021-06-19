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
        public new string OId { get => _id.ToString(); set => _ = value; }       
    }


    public class UserDto
    {
        [BsonIgnore]
        [JsonProperty("_id")]
        public string OId{ get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }      

    }

    public class UserCar : User
    {
        public List<Car> Cars { get; set; }
    }

    public class UserCarDto : UserDto
    {
        public List<CarDto> Cars { get; set; }
    }




}
