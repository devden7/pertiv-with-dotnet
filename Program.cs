using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pertiv_be_with_dotnet.Services;

namespace Pertiv_be_with_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Args: " + string.Join(", ", args)); // Debug log

            var host = CreateHostBuilder(args).Build();

            if (args.Length > 0 && args[0] == "seed-admin") // ✅ Perbaikan di sini
            {
                using (var scope = host.Services.CreateScope())
                {
                    var adminSeed = scope.ServiceProvider.GetRequiredService<AdminAccountSeeder>();
                    adminSeed.CreateAccount();
                    Console.WriteLine("✅ Admin account seeded successfully!");
                }

                Environment.Exit(0);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
