using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityServer4.Stores;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using IdentityServer.Services;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Validation;
using IdentityServer.Validator;
using IdentityServer4.Services;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you wan to add an MVC-based UI
            //services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //var builder = services.AddIdentityServer()
            //    .AddInMemoryIdentityResources(Config.GetIdentityResources())
            //    .AddInMemoryApiResources(Config.GetApis())
            //    .AddInMemoryClients(Config.GetClients());

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<ProfileService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<UserConnection>(c => c.UseSqlServer("Data Source = MSSQLSERVER; Server = LAPTOP-49EKNLEF;Initial Catalog= remuseDB; Integrated Security = True"));

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //if (Environment.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            // uncomment if you want to support static files
            //app.UseStaticFiles();

            app.UseIdentityServer();

            // uncomment, if you wan to add an MVC-based UI
            //app.UseMvcWithDefaultRoute();
        }
    }
}