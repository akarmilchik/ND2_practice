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
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using WebApiContrib.Core.Formatter.Csv;
using Microsoft.Net.Http.Headers;

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
            services.AddControllersWithViews().AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(null);
            })
            .AddViewLocalization();

            services.AddMvc().AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>()).AddCsvSerializerFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddMvcOptions(opts =>
                {
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("csv", new MediaTypeHeaderValue("text/csv"));
                });

            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(Configuration.GetConnectionString("StoreConnection")));

            services.AddScoped<ITicketsService, TicketsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<IVenuesService, VenuesService>();
            services.AddScoped<IUsersAndRolesService, UsersAndRolesService>();

            services.AddDbContext<StoreContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("StoreConnection"))
                .EnableSensitiveDataLogging();
            });

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StoreConnection")));

            services.AddDefaultIdentity<StoreUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreContext>();

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

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSwaggerGen(c => {
                /*
                c.SwaggerDoc("API v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TicketsResale API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),

                    }
                });*/

                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path);
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

            app.UseSwagger();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU"),
                new CultureInfo("be-BY")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketsResale API v1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
