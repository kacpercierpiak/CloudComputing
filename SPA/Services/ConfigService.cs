﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA.Services
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
            ParseData(parsVal);
        }

        public DbCredentials()
        {
            if (Environment.GetEnvironmentVariable("DATABASE_URL") != null)
            {
                var parsVal = new Uri(Environment.GetEnvironmentVariable("DATABASE_URL"));

                ParseData(parsVal);
            }
        }

        private void ParseData(Uri parsVal)
        {
            var userinfo = parsVal.UserInfo.Split(':');
            dbHost = parsVal.Host;
            dbPort = parsVal.Port.ToString();
            dbName = parsVal.Segments[1];
            dbUsername = userinfo[0];
            dbPassword = userinfo[1];
        }

        public string GetDBConnectionString()
        {
            if (dbHost != "")
                return $"Host = {dbHost}; Port = {dbPort}; Database = {dbName}; Username = {dbUsername}; Password = {dbPassword} ;SSL Mode=Require;Trust Server Certificate=true";
            return $"Host=localhost;Port=5433;Database=CloudComputing;Username=web_user;Password=12345";
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
