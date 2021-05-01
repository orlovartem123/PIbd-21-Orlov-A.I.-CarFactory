using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WarehouseEmployeeApp
{
    public class Program
    {
        public static bool? Enter { get; set; }

        public static string CurrentPassword { get; set; }

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
