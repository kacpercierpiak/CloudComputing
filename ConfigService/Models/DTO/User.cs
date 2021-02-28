using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigService.Models.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Car> Cars { get; set; }
    }
}
