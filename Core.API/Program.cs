using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Core.API
{
    public class Program
    {
        public const string ApiPrefix = "api";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
