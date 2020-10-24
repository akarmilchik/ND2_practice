using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Models;
using TicketsResale.Models.Service;

namespace TicketsResale
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            //services.Configure<AdoOptions>(Configuration.GetSection(nameof(AdoOptions)));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.LoginPath = "/User/Login";
                    opts.AccessDeniedPath = "/User/Login";
                    opts.Cookie.Name = "AuthShop";
                });

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });
            
            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<ITicketsCartService, TicketsCartService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<CartIdHandler>();


            services.AddScoped<EventTickets>();

            var dataSource = Configuration.GetValue<string>("DataSource");

            services.AddDbContext<StoreContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("StoreConnection"))
                .EnableSensitiveDataLogging();
            });
                    
           

            services.AddDefaultIdentity<StoreUser>().AddEntityFrameworkStores<StoreContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<AntiforgeryOptions>(opts =>
            {
                opts.FormFieldName = "TicketsStoreSecretInput";
                opts.HeaderName = "X-CSRF-TOKEN";
                opts.SuppressXFrameOptionsHeader = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            var supportedLocales = new[] { "en-US", "ru-RU" , "be-BY"};

            var localizationOptions = new RequestLocalizationOptions().
                            SetDefaultCulture(supportedLocales[0])
                            .AddSupportedCultures(supportedLocales)
                            .AddSupportedUICultures(supportedLocales);

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseMiddleware<CartIdHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
