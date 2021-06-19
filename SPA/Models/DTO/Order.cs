using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Models.DTO
{
    public enum OrderStatus
    {
        New,
        InProgress,
        OnHold,
        Done
    }
    public class Order : OrderDto
    {
        [BsonId]
        public ObjectId _id { get; set; }       
      
     
        public new string OId { get => _id.ToString(); set => _ = value; }

    }

    public class OrderDto: CarHistory
    {
        [BsonIgnore]
        [JsonProperty("_id")]
        public string OId { get; set; }
        public string UserOId { get; set; }
        public string CarOId { get; set; }

    }
    public class CarHistory
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Cost { get; set; }
        public List<Part> Parts { get; set; }
        public string Comment { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
    public class Part
    {
        public string PartName { get; set; }
        public string Brand { get; set; }
        public string CatalogNumber { get; set; }
        public int Qty { get; set; }        
    }
}
