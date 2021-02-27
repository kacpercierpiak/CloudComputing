using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudComputing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
           

            var host = CreateHostBuilder(args).
               UseKestrel().
               UseUrls("https://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT")).
               Build();

            using (var scope = host.Services.CreateScope())
            {
                // extra configuration
            }

            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
