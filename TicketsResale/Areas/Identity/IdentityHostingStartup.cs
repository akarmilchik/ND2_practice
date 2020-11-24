using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TicketsResale.Areas.Identity.IdentityHostingStartup))]
namespace TicketsResale.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}