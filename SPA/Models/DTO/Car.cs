using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPA.Models.DTO
{
    public enum FuelTypes 
    { 
        Gasoline,
        Diesel,
        LPG,
        Hybrid,
        Electric
    }
    public class Car : CarDto
    {

        [BsonId]
        public ObjectId? _id { get; set; }        
        public new string OId { get => _id.ToString(); set => _ = value; }

    }

    public class CarDto
    {
        [BsonIgnore]
        [JsonProperty("_id")]
        public string OId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ProductionDate { get; set; }
        public string VinNo { get; set; }
        public string Engine { get; set; }
        public FuelTypes FuelType { get; set; }
        public bool ManualGearbox { get; set; }
        public string NumberPlate { get; set; }
        public List<CarHistory>? CarHistories { get; set; }
    }



}
