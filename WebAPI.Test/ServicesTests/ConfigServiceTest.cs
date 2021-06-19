using System;
using Xunit;
using WebAPI.Repositories;

namespace WebAPI.Test
{
    public class ConfigServiceTest
    {
     
        [Fact]
        public void CredentialMappingTest()
        {
            
            var dbHost = "Host";
            var dbPort = "5433";
            var dbName = "testDb!1";
            var dbUsername = "username1!A";
            var dbPassword = "password1!A";
            var URI = $"postgres://{dbUsername}:{dbPassword}@{dbHost}:{dbPort}/{dbName}";

            var dbCredential = new DbCredentials(URI);

            Assert.Equal(dbHost.ToLower(), dbCredential.dbHost);
            Assert.Equal(dbPort, dbCredential.dbPort);
            Assert.Equal(dbName, dbCredential.dbName);
            Assert.Equal(dbUsername, dbCredential.dbUsername);
            Assert.Equal(dbPassword, dbCredential.dbPassword);

        }
    }
}
