using SPA.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SPA.Test.Repositories
{

    public class UserRepositoryTest
    {
        private UsersRepository _usersRepository { get; set; }
        public UserRepositoryTest() {
            _usersRepository = new UsersRepository();
        }
        [Fact]
        public void Test1()
        {
           // var users = _usersRepository.GetUsers();
           // Assert.True(users.Count == 2);
        }
    }
}
