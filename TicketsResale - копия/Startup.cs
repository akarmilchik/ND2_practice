using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using TicketsResale.Business.Models;
using TicketsResale.Context;
using TicketsResale.Filters;
using TicketsResale.Mapper;
using TicketsResale.Models;
using TicketsResale.Models.Service;
using TicketsResale.Queries;
using WebApiContrib.Core.Formatter.Csv;

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
                    opts.Filters.Add(typeof(CacheFilterAttribute));
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("csv", new MediaTypeHeaderValue("text/csv"));
                })
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddRazorRuntimeCompilation();

            services.AddTransient<IStringLocalizer, EFStringLocalizer>();
            services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(Configuration.GetConnectionString("StoreConnection")));

            services.AddScoped<CacheFilterAttribute>();
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
                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path);
            });

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(MappingProfile));

            services.Scan(scan => scan
                .FromAssemblyOf<BaseQuery>()
                .AddClasses(c => c.AssignableTo(typeof(ISortingProvider<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
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

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(Cultures.supportedCultures[0])
                .AddSupportedCultures(Cultures.supportedCultures)
                .AddSupportedUICultures(Cultures.supportedCultures);

            app.UseRequestLocalization(localizationOptions);

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
