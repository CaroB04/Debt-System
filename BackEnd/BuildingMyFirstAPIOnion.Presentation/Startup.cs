using BuildingMyFirstAPIOnion.BL.Config;
using BuildingMyFirstAPIOnion.Core.Settings;
using BuildingMyFirstAPIOnion.Services.IoC;
using BuildingMyFirstAPIOnion.Presentation.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BuildingMyFirstAPIOnion.Presentation
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

            #region CORS

            services.AddCors(options =>
            {
                options.AddPolicy("MainPolicy",
                      builder =>
                      {
                          builder
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();

                          //TODO: remove this line for production
                          builder.SetIsOriginAllowed(x => true);
                      });
            });

            #endregion

            services.ConfigSqlServer("Server=LAPTOP-HJFNRGE0\\SQLEXPRESS;Database=DebtSystem;Trusted_Connection=True");

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddServiceRegistry();

            services.AddControllers(options => options.EnableEndpointRouting = false).ConfigFluentValidation();
            //services.AddRazorPages();
            services.AddSwaggerConfig();
            services.ConfigAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAppSwagger();
            }

            app.UseHttpsRedirection();

            app.UseCors("MainPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
