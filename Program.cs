using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HolisticAccountant
{
    public class Program
    {
            
        public static void Main(string[] args)
        {
            // [Serilog] Setting up Serilog, this will allow us to read the configurations for serilog from appsetting.json
             var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Holistic Accountant Web api is starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error encountered while trying to start up Holistic Accountant Web api");
            }
            finally
            {
                Log.CloseAndFlush(); // [Serilog] To handle pending log messages
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // [Serilog] This will allow us to use Serilog as our default logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
