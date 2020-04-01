using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // Default configuration is constructed. That includes app configuration, logging, default server and few other settings.
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.Sources.Clear(); // Removing the default configuration options
            builder.AddJsonFile("config.json", false, true) // System requires a file called config.json
                   .AddXmlFile("config.xml", true)
                   .AddEnvironmentVariables();
        }
    }
}
