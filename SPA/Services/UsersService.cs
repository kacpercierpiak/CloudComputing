using SPA.Models.DTO;
using SPA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository { get; set; }
        public UsersService()
        {
            _usersRepository = new UsersRepository();
        }
        public List<User> GetUsers() { return null; }
    }
}
