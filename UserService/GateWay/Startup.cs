using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWay.Routes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GateWay
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Router router = new Router("routes.json");

            app.Run(async (context) =>
            {
                var content = await router.RouteRequest(context.Request);         //----HttpContext context
                await context.Response.WriteAsync(await content.Content.ReadAsStringAsync());

                //await context.Response.WriteAsync("Hello Armenia");
            });
        }
    }
}
