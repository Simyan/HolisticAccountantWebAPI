using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HolisticAccountant.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HolisticAccountant.Interfaces;
using HolisticAccountant.Repositories;
using Serilog;

namespace HolisticAccountant
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
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
               //builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
               builder.WithOrigins("https://holistic-accountant.herokuapp.com").AllowAnyMethod().AllowAnyHeader();
            }));
            //services.AddCors();
            services.AddDbContext<HolisticAccountantContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("HolisticAccountantConnection")));

            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           app.UseCors("ApiCorsPolicy");
            //app.UseCors(builder => builder
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader()); 
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

           

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
