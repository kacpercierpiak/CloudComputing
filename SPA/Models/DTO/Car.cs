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
    public class Car
    {    
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ProductionDate { get; set; }
        public string VinNo { get; set; }
        public string Engine { get; set; }
        public FuelTypes FuelType { get; set; }
        public bool ManualGearbox { get; set; }
        public string NumberPlate { get; set; }
        public List<CarHistory> CarHistories { get; set; }
    }
   


}
