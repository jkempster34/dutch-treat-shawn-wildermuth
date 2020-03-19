using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
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

            }

            app.UseStaticFiles();
            app.UseNodeModules();

            /* 
             * When a URL that hits the app, it is interpreted and mapped to the correct controller.
             * In previous versions, every feature (MVC, Razor Pages, SIgnalR, etc.), had it�s own endpoint implementation. Now 
             * the endpoint and routing configuration can be done independently.
             * 
             * UseRouting turns on generic endpoint routing in ASP.net Core 3+. It defines where, in the middleware pipeline,
             * the routing decisions are made.
             * 
             * UseEndpoints creates a data structure containing all patterns when the application starts. In other words,
             * it declares the mapping of effective routes
             * 
             * MapControllerRoute defines default mapping. The first parameter is a unique name given to a route, the second specifies
             * a URL pattern, the third are defaults
             * 
             
             */

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Fallback",
                    pattern: "{controller}/{action}/{id?}", // URL will be <root>/ UNFINISHED COMMENT
                    defaults: new { controller = "App", action = "Index" }
                    );
            });
        }
    }
}
