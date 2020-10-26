using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TicketsResale.Context;
using Microsoft.AspNetCore.Identity;
using TicketsResale.Business.Models;

namespace TicketsResale
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreContext>();
            var userManager = services.GetRequiredService<UserManager<StoreUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var seeder = new DataSeeder(context, userManager, roleManager);
            await seeder.SeedDataAsync();
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
