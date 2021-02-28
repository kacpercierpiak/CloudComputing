using System;

namespace ConfigService
{
    public class DbCredentials
    {
        public string dbHost { get; set; } = "";
        public string dbPort { get; set; } = "";
        public string dbName { get; set; } = "";
        public string dbUsername { get; set; } = "";
        public string dbPassword { get; set; } = "";

        public DbCredentials(string uri)
        {
            var parsVal = new Uri(uri);
            var userinfo = parsVal.UserInfo.Split(':');
            dbHost = parsVal.Host;
            dbPort = parsVal.Port.ToString();
            dbName = parsVal.Segments[1];           
            dbUsername = userinfo[0];
            dbPassword = userinfo[1];
        }

    }
    public class Settings
    {
        public string GetDBConnectionString()
        {
            var dbHost = Environment.GetEnvironmentVariable("PORT");
            var dbPort = "5433";
            var dbName = "";
            var dbUsername = "";
            var dbPassword = "";
            return $"Host = {dbHost}; Port = {dbPort}; Database = {dbName}; Username = {dbUsername}; Password = {dbPassword}";

        }
    }
}
