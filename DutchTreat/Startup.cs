using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        // IConfiguration injected into StartUp constructor; it is already pre-configured in Program.cs
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(configuration =>
            {
                configuration.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<DutchContext>();

            services.AddDbContext<DutchContext>(configuration =>
            {
                configuration.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            services.AddTransient<DutchSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IMailService, NullMailService>(); // Whenever the type 'IMailService' is asked for we get a new instance of the concrete 'NullMailService' type
            // Support for real mail service would go here 

            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) // Project > properties > debug > environment variables
            {
                app.UseDeveloperExceptionPage(); // Shows developer error page in browser
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseAuthentication();

            /* 
             * When a URL hits the app, it is interpreted and mapped to the correct controller.
             * In previous versions, every feature (MVC, Razor Pages, SIgnalR, etc.), had it’s own endpoint implementation. Now 
             * the endpoint and routing configuration can be done independently.
             * 
             * UseRouting() turns on generic endpoint routing in ASP.net Core 3+. It defines where, in the middleware pipeline,
             * the routing decisions are made.
             * 
             * UseEndpoints() creates a data structure containing all patterns when the application starts. In other words,
             * it declares the mapping of effective routes.
             * 
             * MapControllerRoute defines default mapping. The first parameter is a unique name given to a route, the second specifies
             * a URL pattern, the third are defaults.
             * 
             */

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Fallback",
                    pattern: "{controller}/{action}/{id?}", // URL with parameters
                                                            // controller refers to contents of controllers folder (.cs file name with "contoller" suffix removed), action refers to the contents of that controller.
                                                            // For example, going to <root>/App/Index will look in the controllers folder for "AppController", and within AppController, look for the "Index" action.
                    defaults: new { controller = "App", action = "Index" } // Parameter defaults. Default URL will be <root>/app/index
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
