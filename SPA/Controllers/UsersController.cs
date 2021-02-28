using SPA.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService _usersService { get; set; }
        public UsersController()
        {
            _usersService = new UsersService();
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _usersService.GetUsers();
        }
    }
}
