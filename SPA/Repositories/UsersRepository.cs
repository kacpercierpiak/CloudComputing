using SPA.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Repositories
{
    
    public class UsersRepository
    {
        public List<User> GetUsers() 
        {
            var userList = new List<User>();
            var car1 = new Car() { Id = 1, Brand = "Ferrari", Model = "Roma", ProductionDate = DateTime.Parse("20.02.2020"), Engine = "4.4 tsi", FuelType = FuelTypes.Gasoline, VinNo = "" };
            var car2 = new Car() { Id = 2, Brand = "Lambo", Model = "Roma", ProductionDate = DateTime.Parse("20.02.2019"), Engine = "4.4 tsi", FuelType = FuelTypes.Gasoline, VinNo = "" };
            
            var carList = new List<Car>();
            carList.Add(car1);
            carList.Add(car2);
            
            userList.Add(new User() {Id=1, Cars=carList, FirstName="Kacper", LastName="Biesiada", Phone="535667432" });

            car1 = new Car() { Id = 1, Brand = "Citroen", Model = "Roma", ProductionDate = DateTime.Parse("20.02.2020"), Engine = "4.4 tsi", FuelType = FuelTypes.Gasoline, VinNo = "" };
            car2 = new Car() { Id = 2, Brand = "BMW", Model = "Roma", ProductionDate = DateTime.Parse("20.02.2019"), Engine = "4.4 tsi", FuelType = FuelTypes.Gasoline, VinNo = "" };

            carList = new List<Car>();
            carList.Add(car1);
            carList.Add(car2);

            userList.Add(new User() { Id = 1, Cars = carList, FirstName = "Tomasz", LastName = "Bis", Phone = "535667432" });
            return userList;
        }
        

    }
}
