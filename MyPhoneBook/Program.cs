using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;

namespace MyPhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();
            Log.Information(messageTemplate: "Application Started");
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally 
            {

                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                }).ConfigureLogging(logging =>
                {
                    // clear default logging providers
                    logging.ClearProviders();

                    // add built-in providers manually, as needed 
                    //logging.AddConsole();  
                    logging.AddSerilog();
                   // logging.AddDebug();
                   // logging.AddEventLog();                   
                });

        public static void ConfigureLogger() 
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(path: @"log.txt", rollingInterval: RollingInterval.Day)
                //.WriteTo.File(new JsonFormatter(), "log-{Date}.json")
                .CreateLogger();

        
        }
    }
}
