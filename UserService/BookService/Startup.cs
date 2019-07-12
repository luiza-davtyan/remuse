using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookService.Models;
using BookService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<BooksDbSettings>(
            Configuration.GetSection(nameof(BooksDbSettings)));

            services.AddSingleton<IBooksDbSettings>(sp =>
            sp.GetRequiredService<IOptions<BooksDbSettings>>().Value);

            services.AddSingleton<BookRepository>();
            services.AddSingleton<AuthorRepository>();
            services.AddSingleton<GenreRepository>();
            //services.AddScoped<IBookRepository, BookRepository>();

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
